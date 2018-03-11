using System;
using System.IO;
using System.Linq;

namespace GCSENEACodeCSharpPort
{
    class Exceptions
    {
        public static void NoUserFolderCatch()
        {
            string root = FileOps.GetRoot();
            string r = FileOps.GetCustomUserFolder(root);

            if (Directory.Exists(r) == false)
            {
                Console.WriteLine("User account folder is missing, it will now be recreated");
                string p = @"C:\NeaFolderData";
                DirectoryInfo d = new DirectoryInfo(p);
                d.Delete(true);
                Console.WriteLine("");
                Console.WriteLine("You will now run through the first time setup again, press any key to continue");
                Console.ReadKey();
                FirstTimeSetup.MainFTS();
            }
            return;
        }

        public static void NeaFolderDataCheck()
        {
            bool do1 = false;
            string root = FileOps.GetRoot();

            if (!Directory.Exists(@"C:\NeaFolderData"))
            {
                do
                {
                    Console.WriteLine("Is this your first time running the software? Yes or No?");
                    string firstRunAnswer = Console.ReadLine();
                    firstRunAnswer.ToLower();

                    if (firstRunAnswer == "yes")
                    {
                        Console.WriteLine("Running first time setup...");
                        FirstTimeSetup.MainFTS();
                    }
                    else if (firstRunAnswer == "no")
                    {
                        Console.Clear();
                        Console.WriteLine("Error: NeaFolderData not found");
                        ApplicationDriveChangeFix();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter yes or no");
                        Console.WriteLine(" ");
                        do1 = true;
                    }
                } while (do1 == true);
            }
        }

        public static void ApplicationDriveChangeFix()
        {
            Console.Clear();
            Console.WriteLine("Your NeaFolderData was not found. You will run through the first time setup again");
            Console.WriteLine("NOTE: If your user account folder still exists, please enter the folder name again, no data will be lost");
            Console.WriteLine("Press enter to continue");
            Console.ReadKey();

            FirstTimeSetup.MainFTS();
        }
    }
}
