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
            string userName = Login.userName;

            if (!Directory.Exists(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Scores"))
            {
                Directory.CreateDirectory(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Scores");
                File.Create(FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Scores" + @"\UserScores.txt").Close();
            }

            string[] arrayForGrade = new string[5] { "1c", "2c", "3c", "4c", "5c" };

            string dir = FileOps.GetCustomUserFolder(FileOps.GetRoot()) + userName + @"\Scores" + @"\UserScores.txt";

            float percentage = score / total * 100;

            int scoreForGrade = score - 1;

            string forAppend = "You scored " + score + " with a percentage of " + percentage + "% and a grade of " + arrayForGrade[scoreForGrade] + ". Subject: " + subject + " | Difficulty: " + difficulty + " at " + DateTime.Now; //Fucking horrible why does it force me to do this TRY TO FIND A WAY TO NOT DO THIS
            File.AppendAllText(dir, forAppend + Environment.NewLine);

            bool forEndLoop = false;

            do
            {
                forEndLoop = false;
                Console.WriteLine("");
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1. Play the quiz again");
                Console.WriteLine("2. Choose a different quiz");
                Console.WriteLine("3. Log-out");
                Console.WriteLine("4. Quit");
                int endOfQuizChoice = Convert.ToInt32(Console.ReadLine());

                switch (endOfQuizChoice)
                {
                    case 1:
                        Console.Clear();
                        Questions.QuestionsMain();
                        break;
                    case 2:
                        Console.Clear();
                        Difficulty.Choice();
                        break;
                    case 3:
                        Console.Clear();
                        Login.SignIn();
                        break;
                    case 4:
                        Environment.Exit(1);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("You did not enter a correct response");
                        forEndLoop = true;
                        break;
                }
            } while (forEndLoop == true);

            Console.ReadKey();
        }

    }
}
