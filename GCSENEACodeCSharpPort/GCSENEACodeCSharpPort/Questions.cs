using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GCSENEACodeCSharpPort
{
    class Questions
    {
        public static void QuestionsMain(string[] difficultyAndSubjectArray)
        {
            string root = FileOps.GetRoot();
            string questionAnwserDifficultyDir = @"Questions\" + difficultyAndSubjectArray[0] + @"\" + difficultyAndSubjectArray[1] + @"\";
            int score = 0;
            int totalForPercent = 0;
            int line = 1;
            int numberOfLines = Countlines(questionAnwserDifficultyDir);

            Console.WriteLine("Press enter to start your quiz");
            Console.ReadKey();
            Console.Clear();

            for (int i = 0; i < numberOfLines; i++)
            {
                string[] questionAnswer = QAGet(root, questionAnwserDifficultyDir, line);

                Console.WriteLine(questionAnswer[0]);
                string userRespone = Console.ReadLine();

                if (userRespone == questionAnswer[1])
                {
                    Console.WriteLine("Correct");
                    score++;
                    totalForPercent++;
                    line++;
                }
                else if (userRespone != questionAnswer[1])
                {
                    Console.WriteLine("False");
                    totalForPercent++;
                    line++;
                }
            }

            AfterQuestions.DisplayUserScore(score, totalForPercent, difficultyAndSubjectArray[0], difficultyAndSubjectArray[1]);
        }

        static string[] QAGet(string root, string QADir, int line)
        {
            string[] questionAnswers = new string[2];
            string[] valsForQA = new string[2] { "Questions.txt", "Answers.txt" };
            int i = 0;

            string userDir = FileOps.GetCustomUserFolder(root);

            foreach (string valQA in valsForQA)
            {
                if (line >= 2)
                {
                    int lineSkip = line;
                    lineSkip--;

                    questionAnswers[i] = File.ReadLines(userDir + QADir + valQA).Skip(lineSkip).Take(line).First();
                    i++;
                }
                else
                {
                    questionAnswers[i] = File.ReadLines(userDir + QADir + valQA).Take(line).First();
                    i++;
                }
            }

            return questionAnswers;
        }

        static int Countlines(string QADir)
        {
            string root = FileOps.GetRoot();
            string userDir = FileOps.GetCustomUserFolder(root);

            int i = File.ReadAllLines(userDir + @"\" + QADir + "Answers.txt").Count();
            int j = File.ReadAllLines(userDir + @"\" + QADir + "Questions.txt").Count();

            if (i != j)
            {
                Console.WriteLine("Warning: Amount of questions and answers does not match");
                if (i > j)
                {
                    return i;
                }
                else if (j > i)
                {
                    return j;
                }
            }

            return i;
        }
    }
}
