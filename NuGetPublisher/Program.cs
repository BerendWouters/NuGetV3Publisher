using System;
using System.Threading.Tasks;

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

            HandleOptions(nugetRepo);
            
            Console.ReadLine();
        }

        private static async Task HandleOptions(NuGetResource nugetRepo)
        {
            var option = AskQuestion("Two options:" + Environment.NewLine + "1. List packages" + Environment.NewLine + "2. Add package");

            while (!string.IsNullOrEmpty(option))
            {
                if (option == "1")
                {

                    var searchString = AskQuestion("Enter searchpattern");
                    var packages = await nugetRepo.SearchPackages(searchString);
                    foreach (var packageSearchMetadata in packages)
                    {
                        Console.WriteLine($"{packageSearchMetadata.Title} - {packageSearchMetadata.Description}");
                    }
                }
                else
                {
                    await nugetRepo.UploadPackage();
                }

                option = AskQuestion("Two options: 1. List packages" + Environment.NewLine + "2. Add package");
            }
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
