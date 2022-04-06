using System;
using AnkhMorpork;

namespace OOPExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scenario = new ScenarioCreator();

            ConsoleViewer.ShowWelcomeWord();

            //Get player's name
            var player = new Player("Viktor");            
                       
            scenario.InitialiseBeggarsGuild(@"");
            scenario.InitialiseFoolsGuild(@"");

            //for (int i = 0; i < 50; i++)
            //    Console.WriteLine(scenario.CreateRandomGuildMeeting());

            do
            {
                ConsoleViewer.ShowCurrentBudget(player.CurrentBudget);
                var meeting = scenario.CreateRandomGuildMeeting();
                Console.WriteLine(meeting);
                ConsoleViewer.ShowChoose();
                var choice = Console.ReadLine();
                if (choice == "1")
                    scenario.Accept(player);
                if(choice == "2")
                    scenario.Skip(player);

            }while(player.IsAlive);

            ConsoleViewer.ShowScore(player.ToString());

            Console.ReadKey();
        }
    }
}
