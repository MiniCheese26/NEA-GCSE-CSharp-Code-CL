using System;
using System.IO;

namespace GCSENEACodeCSharpPort
{
    class FileOps
    {
        public static string GetUserDir(string inputUserName)
        {
            string customDirectory = GetCustomUserFolder(GetRoot());
            string dir = customDirectory + inputUserName; //Creates a string for the directory location
            string v = GetUserData(dir);

            return v; //Returns value
        }

        public static void FileWriter(string dir, string[] userDataArray)
        {
            string[] fileNames = new string[5] { "Name", "Password", "Age", "Year Group", "Username" }; //Array for passing file names inside for loop
            string userName = userDataArray[4]; //Used to create users directory

            FileStream fileCreate;

            userName = userName + @"\";

            for (int i = 0; i < 5; i++) //Goes until i = 5
            {
                string p = fileNames[i] + ".txt"; //Creates full filenames for each loop so the file it's writing to is automatic
                fileCreate = File.Create(dir + userName + p); //Creates the files in the correct directory
                fileCreate.Close();

                StreamWriter fileStreamWriter = new StreamWriter(dir + userName + p); //Open fileStreamWriter in the current user directory

                fileStreamWriter.WriteLine(userDataArray[i]); //Write the data the user entered to the correct file
                fileStreamWriter.Close();
            }

            return;
        }

        public static void MainFW(string[] userData)
        {
            string dir = GetCustomUserFolder(GetRoot());

            bool dirCheck = Directory.Exists(dir + userData[4] + @"\"); //Makes sure the user account doesn't already exist

            if (dirCheck == true) //If it does move them along to Login.SignIn()
            {
                Console.WriteLine(" ");
                Console.WriteLine("User account already exists");
                Console.WriteLine(" ");
                Login.SignIn();
            }

            Directory.CreateDirectory(dir + userData[4] + @"\"); //Create directory for user
            Directory.CreateDirectory(dir + "Questions");
            FileWriter(dir, userData);
            Login.SignIn();
        }

        public static string GetUserData(string dir) //Has to be a string to account for capitals
        {
            if (!File.Exists(dir))
            {
                Console.Clear();
                Console.WriteLine("Incorrect, please try again");
                return "False";
            }
            else
            {
                string userName;
                StreamReader ReadUserName = new StreamReader(dir); //Opens the text from dir
                userName = Convert.ToString(ReadUserName.ReadLine()); //Converts data read by ReadUserName into string and stores it 
                ReadUserName.Close();
                return userName; //Would've used file.readallLines but it would pick up the whitespace and .trim and .remove wouldn't work for some reason.
            }
        }

        public static string GetCustomUserFolder(string root)
        {
            string location;

            using (StreamReader r = new StreamReader(@"C:\NeaFolderData\path.txt"))
            {
                location = r.ReadLine();
            }

            location.Trim();
            location = location + @"\";
            return location;
        }

        public static string GetRoot()
        {
            return Path.GetPathRoot(Directory.GetCurrentDirectory());
        }
    }
}
