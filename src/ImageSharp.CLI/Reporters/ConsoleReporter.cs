using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageSharp.CLI.Reporters
{
    public class ConsoleReporter : IReporter
    {
        bool reportedErrors = false;
        public bool ReportedErrors => reportedErrors;

        public void CompleteExecution(ImageOperationContext context, string errorMessage)
        {
            TextWriter writer = Console.Out;
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var old = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Out.WriteLine($"[{DateTime.Now}] Completed running '{context.Name}' with '{context.FilePath}'");
                Console.ForegroundColor = old;
            }
            else
            {
                reportedErrors = true;
                var old = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Out.WriteLine($"[{DateTime.Now}] Errored running '{context.Name}' with '{context.FilePath}'");
                writer.WriteLine(errorMessage);
                Console.ForegroundColor = old;
            }
            
        }

        public void StartExecution(ImageOperationContext context)
        {
            Console.Out.WriteLine($"[{DateTime.Now}] Running running '{context.Name}' with '{context.FilePath}'");
        }
    }
}
