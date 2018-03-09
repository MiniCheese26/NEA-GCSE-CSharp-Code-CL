using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GCSENEACodeCSharpPort
{
    class AfterQuestions
    {
        public static void DisplayUserScore(int score, int total, string subject, string difficulty)
        {
            Console.Clear();
            Console.WriteLine("You scored {0}/{1} with a percentage of {2}%", score, total, score / total * 100);
            string userName = FileOps.ReadCurrentUser();

            if (!Directory.Exists(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Answers"))
            {
                Directory.CreateDirectory(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Answers");
                File.Create(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Answers" + @"\UserScores.txt").Close();
            }

            string[] arrayForGrade = new string[5] { "1c", "2c", "3c", "4c", "5c" };

            string dir = FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Answers" + @"\UserScores.txt";

            float percentage = score / total * 100;

            switch (score)
            {
                case 1:
                    string forAppend = "You scored " + score + " with a percentage of " + percentage + " and a grade of " + arrayForGrade[0] + ". Subject: " + subject + " | Difficulty " + difficulty + " at " + DateTime.Now; //Fucking horrible why does it force me to do this
                    File.AppendAllText(dir, forAppend + Environment.NewLine);
                    break;
            }

            Console.ReadKey();
        }

    }
}
