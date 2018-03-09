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
                string p = root + @"NeaFolderData\";
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

            if (!Directory.Exists(root + "NeaFolderData"))
            {
                do
                {
                    Console.WriteLine("Is this your first time running the software? Yes or No?");
                    string firstRunAnswer = Console.ReadLine();
                    firstRunAnswer.ToLower();

                    if (firstRunAnswer == "yes")
                    {
                        Console.WriteLine("Running first time setup...");
                        do1 = false;
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
            return;
        }

        public static void ApplicationDriveChangeFix()
        {
            Console.Clear();
            Console.WriteLine("Your NeaFolderData was not found. If you're running the application from a different drive enter 1");
            Console.WriteLine("If your user accounts folder still exists but NeaFolderData doesn't enter 2");
            int driveErrorValue = Convert.ToInt32(Console.ReadLine());

            bool valueOneLoop = false;
            string realDrive;

            if (driveErrorValue == 1)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine(@"Please enter the drive your data was originally on (I.E. C:\)");
                    realDrive = Console.ReadLine();

                    if (!Directory.Exists(realDrive + "NeaFolderData"))
                    {
                        valueOneLoop = IncorrectRealDrive();
                    }
                    else
                    {
                        valueOneLoop = false;
                    }
                } while (valueOneLoop == true);

                string root = FileOps.GetRoot();

                Directory.CreateDirectory(root + "NeaFolderData");

                File.Create(root + "NeaFolderData" + @"\path.txt").Close();

                StreamReader reader = new StreamReader(realDrive + "NeaFolderData" + @"\path.txt");
                string originalPath = reader.ReadLine().Trim();
                reader.Close();

                StreamWriter writer = new StreamWriter(root + "NeaFolderData" + @"\path.txt");
                writer.WriteLine(originalPath);
                writer.Close();

                string filesFoundAtString = realDrive + "NeaFolderData" + @"\path.txt";

                Console.Clear();
                Console.WriteLine("Your files where found at {0}", filesFoundAtString);
                Console.WriteLine("Operation complete!");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                Console.Clear();

                Start.Main();
            }
            else if (driveErrorValue == 2)
            {
                Console.Clear();
                Console.WriteLine("You will now go through the first time setup again");
                Console.WriteLine("Enter the ORIGINAL folder NAME for your user data, NO DATA WILL BE LOST");
                Console.WriteLine("Enter any key to continue");
                Console.ReadKey();

                FirstTimeSetup.MainFTS();
            }
        }

        static bool IncorrectRealDrive()
        {
            Console.Clear();
            Console.WriteLine("Your userdata was not found, it may have been wiped on that drive");
            Console.WriteLine("Please check you entered the correct drive");
            Console.WriteLine("If the folder does not exist, please enter 'retry' to go back and choose option 2");
            Console.WriteLine("Else just press any key");
            string realDriveReturnOption = Console.ReadLine();

            if (realDriveReturnOption == "retry")
            {
                ApplicationDriveChangeFix();
            }
            else
            {
                return true;
            }
            return false; //Maybe return null then write a method to handle the exception? Probably just misusing the return type
        }
    }
}
