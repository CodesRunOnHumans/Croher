using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Croher.Actions
{
    internal class HelpAction : IAction
    {
        public const string ActionNameOfHelpAction = "help";
        public const string ActionClassExtensionName = "Action";
        public const string ActionClassNamespace = "Croher.Actions";

        public ActionHelpMessages HelpMessages => new ActionHelpMessages(
            "To show this page.",
            new Dictionary<string, string>()
        );

        public bool Execute(IDictionary<string, string>? parameters = null)
        {
            Type actionInterface = typeof(IAction);
            var actions = from type in Assembly.GetExecutingAssembly().GetTypes()
                          where type.Namespace is ActionClassNamespace
                          where Array.IndexOf(type.GetInterfaces(), actionInterface) is not -1
                          where type.Name.EndsWith(ActionClassExtensionName)
                          let constructor = type.GetConstructor(Array.Empty<Type>())
                          where constructor is not null
                          let action = (IAction)constructor.Invoke(null)
                          orderby type.Name
                          select (
                          nameWithColon: $"{type.Name[..^ActionClassExtensionName.Length].ToLower()}:",
                          summary: action.HelpMessages.Summary,
                          parameters: action.HelpMessages.Parameters
                          );
            var padding = actions.Max((action) => action.nameWithColon.Length) + 2;
            var parameterPadding = padding + 2;
            foreach (var action in actions)
            {
                Console.Write(action.nameWithColon.PadRight(padding));
                Console.WriteLine(action.summary);
                foreach (var parameter in action.parameters)
                {
                    Console.Write(string.Empty.PadRight(parameterPadding));
                    Console.Write(parameter.Key);
                    Console.Write(": ");
                    Console.WriteLine(parameter.Value);
                }
            }
            return true;
        }

        #region old implementation - directly output, not well-ordered or well-padded
        /*
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
                        Console.WriteLine($"{type.Name[..^6].ToLower(),-6}: {action.HelpMessages.Summary}");
                        foreach (var parameter in action.HelpMessages.Parameters)
                            Console.WriteLine($"{null,-8}{parameter.Key}: {parameter.Value}");
                    }
                }
            return true;
        }
        */
        #endregion

    }
}
