using System;
using System.Collections.Generic;
using System.IO;

namespace Croher.Actions
{
    internal class BuildAction : IAction
    {
        public ActionHelpMessages HelpMessages => new ActionHelpMessages(
            "To compile the codes.",
            new Dictionary<string, string>() {
                { "-in", "Path of the codes to be compiled." },
                { "-out", "Path of the output file." },
            }
        );

        public bool Execute(IDictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("-in", out var input)
                || !parameters.TryGetValue("-out", out var output))
                return false;

            Console.WriteLine("Finding input file...");
            using var inputFile = new FileInfo(input).OpenRead();

            Console.WriteLine("Creating output file...");
            var outputFileInfo = new FileInfo(output);
            using var outputFile = outputFileInfo.OpenWrite();

            Console.WriteLine("Compiling...");
            inputFile.CopyTo(outputFile);
            outputFile.Flush();

            Console.WriteLine($"Succeeded: {outputFileInfo.FullName}");
            return true;
        }
    }
}
