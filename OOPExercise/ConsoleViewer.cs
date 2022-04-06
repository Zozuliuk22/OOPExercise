using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPExercise
{
    internal static class ConsoleViewer
    {
        internal static void ShowWelcomeWord()
        {
            Console.WriteLine("Welcome to the fine city of Ankh-Morpork!");
        }

        internal static void ShowCurrentBudget(decimal budget)
        {
            Console.WriteLine($"Current budget: {Math.Truncate(budget)} AM$ " +
                $"{Math.Truncate((budget - Math.Truncate(budget)) * 100)} pence");
        }

        internal static void ShowChoose()
        {
            Console.WriteLine("Accept - 1\nSkip - 2");
        }

        internal static void ShowScore(string score)
        {
            Console.WriteLine(score);
        }
    }
}
