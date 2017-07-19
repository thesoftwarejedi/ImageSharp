using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSharp.CLI.Reporters
{
    public interface IReporter
    {
        bool ReportedErrors { get; }
        void StartExecution(ImageOperationContext context);
        void CompleteExecution(ImageOperationContext context, string errorMessage);
    }
}
