using Croher.Actions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Croher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintStartingMessages();
            PrintSplitLine();

            var (actionName, parameters) = ParseArguments(args);
            var fullName = $"{HelpAction.ActionClassNamespace}." +
                $"{actionName}{HelpAction.ActionClassExtensionName}";

            var actionClass = Assembly.GetExecutingAssembly().GetType(
                fullName, false, ignoreCase: true) ?? typeof(HelpAction);

            var constructor = actionClass.GetConstructor(Array.Empty<Type>());
            var action = (IAction)(constructor?.Invoke(null) ?? new HelpAction());
            if (!action.Execute(parameters))
                _ = new HelpAction().Execute();

            PrintSplitLine();
            PrintEndingMessages();
        }

        private static void PrintSplitLine(int count = 10, char character = '=')
        {
            Console.WriteLine(string.Empty.PadLeft(count, character));
        }

        private static void PrintEndingMessages()
        {
            Console.WriteLine("Done.");
        }

        private static void PrintStartingMessages()
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine($"{name.Name} v{name.Version?.ToString(3) ?? "ersion: unknown"}");
        }

        private static (string actionName, IDictionary<string, string> parameters) ParseArguments(string[] args)
        {
            Dictionary<string, string> result = new();

            if (args.Length % 2 is 0)
                return ("help", result);

            for (int i = 1; i < args.Length; i++)
                result.Add(args[i], args[++i]);

            return (args[0], result);
        }
    }
}
