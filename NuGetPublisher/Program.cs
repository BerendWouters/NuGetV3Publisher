using System;

namespace NuGetPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = AskQuestion("Provide your username");
            var pat = AskQuestion("Enter your PAT");
            var nugetRepo = new NuGetResource();
            nugetRepo.SetupConnection(username, pat);
            string searchString = String.Empty;
            searchString = AskQuestion("Enter searchpattern");
            while (!string.IsNullOrEmpty(searchString))
            {

                var packages = nugetRepo.SearchPackages(searchString).Result;
                foreach (var packageSearchMetadata in packages)
                {
                    Console.WriteLine($"{packageSearchMetadata.Title} - {packageSearchMetadata.Description}");
                }
                searchString = AskQuestion("Enter searchpattern");
            }
            Console.ReadLine();
        }

        private static string AskQuestion(string question)
        {
            string input = String.Empty;
            while (string.IsNullOrEmpty(input))
            {
                Console.Write($"{question}: ");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}
