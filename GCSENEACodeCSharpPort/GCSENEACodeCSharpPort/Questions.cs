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
            bool loop = true;
            int v = 1;

            do //Any other way to do this? No way to just place a loop at the end like vb
            {
                string[] questionAnswer = QAGet(root, questionAnwserDifficultyDir, v);

                if (questionAnswer == null)
                {
                    AfterQuestions.DisplayUserScore(score, totalForPercent);
                }

                Console.WriteLine(questionAnswer[0]);
                string userRespone = Console.ReadLine();

                if (userRespone == questionAnswer[1])
                {
                    Console.WriteLine("Correct");
                    score++;
                    totalForPercent++;
                    v++;
                }
                else if (userRespone != questionAnswer[1])
                {
                    Console.WriteLine("False");
                    totalForPercent++;
                    v++;
                }
                else
                {
                    Console.WriteLine("???");
                    v++;
                }
            } while (loop == true);


            Console.ReadKey();
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
                    try
                    {
                        int lineS = line;
                        lineS = lineS - 1;

                        questionAnswers[i] = File.ReadLines(userDir + QADir + valQA).Skip(lineS).Take(line).First();
                        i++;
                    }
                    catch (InvalidOperationException) //Fix with method to end if before executing this statement
                    {
                        return null;
                    }
                }
                else
                {
                    questionAnswers[i] = File.ReadLines(userDir + QADir + valQA).Take(line).First();
                    i++;
                }
            }

            return questionAnswers;
        }
    }
}
