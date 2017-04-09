using System;
using NuGet.Common;

namespace NuGetPublisher
{
    public class CustomLogger : ILogger
    {
        public void LogDebug(string data)
        {
            Console.WriteLine($"DEBUG: {data}");
        }

        public void LogVerbose(string data)
        {
            Console.WriteLine($"VERBOSE: {data}");
        }

        public void LogInformation(string data)
        {
            Console.WriteLine($"INFORMATION: {data}");
        }

        public void LogMinimal(string data)
        {
            Console.WriteLine($"MINIMAL: {data}");
        }

        public void LogWarning(string data)
        {
            Console.WriteLine($"WARNING: {data}");
        }

        public void LogError(string data)
        {
            Console.WriteLine($"ERROR: {data}");
        }

        public void LogInformationSummary(string data)
        {
            Console.WriteLine($"INFORMATIONSUMMARY: {data}");
        }

        public void LogErrorSummary(string data)
        {
            Console.WriteLine($"ERRORSUMMARY: {data}");
        }
    }
}