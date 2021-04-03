using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Croher.Actions
{
    class BuildAction : IAction
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
            using var outputFile = new FileInfo(output).OpenWrite();

            Console.WriteLine("Compiling...");
            inputFile.CopyTo(outputFile);
            outputFile.Flush();

            return true;
        }
    }
}
