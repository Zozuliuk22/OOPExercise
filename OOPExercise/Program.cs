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

            scenario.InitialiseAssassinsGuild(@"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\assassins.json");                       
            scenario.InitialiseBeggarsGuild(@"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\beggars.json");
            scenario.InitialiseFoolsGuild(@"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\fools.json");           

            do
            {
                ConsoleViewer.ShowCurrentBudget(player.CurrentBudget);
                var meeting = scenario.CreateRandomGuildMeeting();
                Console.WriteLine(meeting);
                ConsoleViewer.ShowChoose();
                var choice = Console.ReadLine();

                if (meeting.ToLower().Contains("assassin"))
                {
                    if (choice == "1")
                    {
                        Console.Write("Enter fee: ");
                        var fee = Decimal.Parse(Console.ReadLine());
                        scenario.SetEnteredFee(fee);
                        scenario.Accept(player);
                    }
                    if (choice == "2")
                        scenario.Skip(player);
                }
                else
                {
                    if (choice == "1")
                        scenario.Accept(player);
                    if (choice == "2")
                        scenario.Skip(player);
                }                

            }while(player.IsAlive);

            ConsoleViewer.ShowScore(player.ToString());

            Console.ReadKey();
        }
    }
}
