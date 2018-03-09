using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace GCSENEACodeCSharpPort
{
    class AfterQuestions
    {
        public static void DisplayUserScore(int score, int total)
        {
            Console.Clear();
            Console.WriteLine("You scored {0}/{1} with a percentage of {2}%", score, total, score / total * 100);
            Console.ReadKey();
        }
    }
}
