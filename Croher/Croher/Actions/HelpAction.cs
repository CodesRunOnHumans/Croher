using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Croher.Actions
{
    class HelpAction : IAction
    {
        public ActionHelpMessages HelpMessages => new ActionHelpMessages(
            "To show the help pages.",
            new Dictionary<string, string>()
        );

        public bool Execute(IDictionary<string, string>? parameters = null)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                if (type.Namespace is not null
                    && type.Namespace is "Croher.Actions"
                    && Array.IndexOf(type.GetInterfaces(), typeof(IAction)) is not -1)
                {
                    var constructor = type.GetConstructor(Array.Empty<Type>());
                    if (constructor is not null)
                    {
                        var action = (IAction)constructor.Invoke(null);
                        Console.WriteLine($"{type.Name[..^6].ToLower()}: {action.HelpMessages.Summary}");
                        foreach (var parameter in action.HelpMessages.Parameters)
                            Console.WriteLine($"\t{parameter.Key}: {parameter.Value}");
                    }
                }
            return true;
        }
    }
}
