using System;
using AnkhMorpork;

namespace OOPExercise
{
    internal static class ConsoleViewer
    {
        internal static void ShowWelcomeWord()
        {            
            Console.WindowWidth = 150;
            Console.WindowHeight = 30;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\t\t\t\t\tWelcome to the fine city of Ankh-Morpork!\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ankh-Morpork lies on the River Ankh (the most polluted waterway on the Discworld and reputedly solid enough to walk on), " +
                "\nwhere the fertile loam of the Sto Plains meets the Circle Sea. This, naturally, puts it in an excellent trading position.\n");
            Console.WriteLine("The central city divides more or less into Ankh (the posh part) and Morpork (the humble part, " +
                "\nwhich includes the slum area known as \"the Shades\"), which are separated by the River Ankh.\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\t\t\tINSTRUCTIONS\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("There are not lots of rules. All that you must know: You have 100 AM$ at the beginning of the game.");
            Console.WriteLine("In this city you will be go along streets and have meetings with different members of local guilds.");
            Console.WriteLine("Read guidance closely as the game progresses. And my advice for you : Try to survive as long as possible.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("It can be dangerous to walk the streets. So watch out!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

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

        internal static int GetChoice()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int choice = 0;

            while (choice != 1 && choice != 2)
            {
                Console.Write("Please, enter your choice (1 or 2) to continue: ");
                Int32.TryParse(Console.ReadLine(), out choice);
            }

            Console.ForegroundColor = ConsoleColor.White;
            return choice;
        }

        internal static string GetPlayerName()
        {
            var name = String.Empty;

            Console.Write("Hello, dear player! Please, enter your correct name to start the game trip: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            name = Console.ReadLine().Trim();
            Console.ForegroundColor = ConsoleColor.White;

            return name;
        }

        internal static void ShowScore(Player player)
        {
            if (player is null)
                throw new ArgumentNullException("The player value cannot be null.");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\t" + player.ToString());
            Console.ForegroundColor= ConsoleColor.White;
        }         

        internal static void ShowMeetingWelcomeInfo(Meeting meeting)
        {
            if(meeting is null)
                throw new ArgumentNullException("The meeting value cannot be null.");

            Console.Write("\nYou go along the streets of Ankh-Morpork and ... ");
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

        internal static void ShowMeetingResultInfo(string result)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        internal static decimal GetEnteredFee(Player player)
        {
            if (player is null)
                throw new ArgumentNullException("The player value cannot be null.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            decimal fee = 0;

            while (fee <= 0 || fee > player.CurrentBudget)
            {
                Console.Write("Please, enter a fee as much as you want (but not more than you have): ");
                Decimal.TryParse(Console.ReadLine(), out fee);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            return fee;
        }
    }
}
