using System;
using System.Threading.Tasks;
using NuGetPublisher.Management;

namespace NuGetPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = AskQuestion("Provide your username");
            var pat = AskQuestion("Enter your PAT");
            var nugetRepo = new PackageManager(new NuGetConnection(username, pat));

            HandleOptions(nugetRepo);

            Console.ReadLine();
        }

        private static async Task HandleOptions(PackageManager packageManager)
        {
            var option = AskQuestion("Two options:" + Environment.NewLine + "1. List packages" + Environment.NewLine + "2. Add package");

            while (!string.IsNullOrEmpty(option))
            {
                if (option == "1")
                {

                    var searchString = AskQuestion("Enter searchpattern");
                    var packages = await packageManager.SearchPackagesAsync(searchString);
                    foreach (var packageSearchMetadata in packages)
                    {
                        Console.WriteLine($"{packageSearchMetadata.Title} - {packageSearchMetadata.Description}");
                    }
                }
                else
                {
                    await packageManager.UploadPackage();
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
