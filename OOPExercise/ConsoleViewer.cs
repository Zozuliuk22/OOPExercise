using System;
using AnkhMorpork;
using OOPExercise.Properties;

namespace OOPExercise
{
    internal static class ConsoleViewer
    {
        /// <summary>
        /// Get a formatted title for the long phrase.
        /// </summary>
        /// <param name="title">The long phrase to make a formatted title.</param>
        /// /// <returns>The formatted title.</returns>
        private static string GetLongTitle(string title)
        {
            return "\n" + new string('\t', 5) + title + "\n";
        }

        /// <summary>
        /// Get a formatted title for the short phrase
        /// </summary>
        /// <param name="title">The short phrase to make a formatted title.</param>
        /// <returns>The formatted title.</returns>
        private static string GetShortTitle(string title)
        {
            return new string('\t', 7) + title + "\n";
        }

        /// <summary>
        /// Output welcome text and instructions in the console.
        /// </summary>
        internal static void ShowWelcomeWord()
        {
            Console.WindowWidth = 150;
            Console.WindowHeight = 30;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(GetLongTitle(ScenarioTexts.WelcomeTitle));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ScenarioTexts.WelcomeMessagePart1);
            Console.WriteLine(ScenarioTexts.WelcomeMessagePart2 + "\n");
            Console.WriteLine(ScenarioTexts.WelcomeMessagePart3);
            Console.WriteLine(ScenarioTexts.WelcomeMessagePart4 + "\n");
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(GetShortTitle(ScenarioTexts.InstructionsTitle));
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(ScenarioTexts.InstructionsPoint1);
            Console.WriteLine(ScenarioTexts.InstructionsPoint2);
            Console.WriteLine(ScenarioTexts.InstructionsPoint3 + "\n");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(ScenarioTexts.WelcomeWarningMessage + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Output the player's current budget in the console.
        /// </summary>
        /// <param name="budget">The player's current budget.</param>
        internal static void ShowCurrentBudget(decimal budget)
        {
            Console.WriteLine();
            Console.Write("\t\t  ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write($"  Current budget: {Math.Truncate(budget)} AM$ " +
                $"{String.Format("{0 : 00}", Math.Truncate(budget * 100 % 100))} pence   ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\n\n");
        }

        /// <summary>
        /// Output possible choices for a player in the console.
        /// </summary>
        internal static void ShowChoice()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Choices:");
            Console.WriteLine("\t1 - Accept");
            Console.WriteLine("\t2 - Skip");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// Get the player's input choice from the console.
        /// </summary>
        /// <returns>The sequence number of the choice.</returns>
        internal static int GetChoice()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int choice = 0;

            while (choice != 1 && choice != 2)
            {
                Console.Write(ScenarioTexts.EnterChoiceMessage);
                Int32.TryParse(Console.ReadLine(), out choice);
            }

            Console.ForegroundColor = ConsoleColor.White;
            return choice;
        }

        /// <summary>
        /// Get the player's name from the console.
        /// </summary>
        /// <returns>The input player's name.</returns>
        internal static string GetPlayerName()
        {
            var name = String.Empty;

            Console.Write(ScenarioTexts.EnterPlayerNameMessage);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            name = Console.ReadLine().Trim();
            Console.ForegroundColor = ConsoleColor.White;

            return name;
        }

        /// <summary>
        /// Output the player's current score in the console.
        /// </summary>
        /// <param name="player">The object of Player.</param>
        /// <exception cref="ArgumentNullException">The player value cannot be null.</exception>
        internal static void ShowScore(Player player)
        {
            if (player is null)
                throw new ArgumentNullException("The player value cannot be null.");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\t" + player.ToString());
            Console.ForegroundColor= ConsoleColor.White;
        }

        /// <summary>
        /// Output welcome information about the meeting in the console.
        /// </summary>
        /// <param name="meeting">The object of Meeting.</param>
        /// <exception cref="ArgumentNullException">The meeting value cannot be null.</exception>
        internal static void ShowMeetingWelcomeInfo(Meeting meeting)
        {
            if(meeting is null)
                throw new ArgumentNullException("The meeting value cannot be null.");

            Console.Write("\n" + ScenarioTexts.WalkingMessage);

            Console.ForegroundColor= meeting.Guild.GuildColor;
            Console.WriteLine(meeting);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + meeting.Guild.WelcomeMessage); 

            if(meeting.Npc is not null)
            {
                Console.ForegroundColor = meeting.Guild.GuildColor;
                Console.WriteLine("\n" + meeting.Npc.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Output result information about the meeting in the console.
        /// </summary>
        /// <param name="result">The text-result of a meeting.</param>
        internal static void ShowMeetingResultInfo(string result)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(result);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// Get the fee that is entered by the player from the console.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>The fee that is entered by the player.</returns>
        /// <exception cref="ArgumentNullException">The player value cannot be null.</exception>
        internal static decimal GetEnteredFee(Player player)
        {
            if (player is null)
                throw new ArgumentNullException("The player value cannot be null.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            decimal fee = 0;

            while (fee <= 0 || fee > player.CurrentBudget)
            {
                Console.Write(ScenarioTexts.EnterFeeMessage);
                Decimal.TryParse(Console.ReadLine(), out fee);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            return fee;
        }
    }
}
