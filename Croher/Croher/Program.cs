using Croher.Actions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Croher
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintStartMessages();
            PrintSplitLine();
            var (actionName, parameters) = ParseArguments(args);
            var actionType = Assembly.GetExecutingAssembly().GetType(
                $"Croher.Actions.{actionName}Action", false, true) ?? typeof(HelpAction);

            var constructor = actionType.GetConstructor(Array.Empty<Type>());
            var action = constructor?.Invoke(null) ?? new HelpAction();
            if(!((IAction)action).Execute(parameters))
                _ = new HelpAction().Execute();

            PrintSplitLine();
            Console.WriteLine("Work Finished.");
        }

        static void PrintSplitLine(int count = 10, char character = '=')
        {
            for (int i = 0; i < count; i++)
                Console.Write(character);
            Console.WriteLine();
        }
        static void PrintStartMessages()
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine($"{name.Name} v{name.Version?.ToString(3) ?? "unknown"}");
        }
        static (string actionName, IDictionary<string, string> parameters) ParseArguments(string[] args)
        {
            Dictionary<string, string> result = new();

            if (args.Length % 2 is 0)
                return ("help", result);

            for (int i = 1; i < args.Length; i++)
            {
                result.Add(args[i], args[++i]);
            }
            return (args[0], result);
        }
    }
}
