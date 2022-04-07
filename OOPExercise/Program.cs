using System;
using AnkhMorpork;

namespace OOPExercise
{
    internal class Program
    {
        private const string _pathToAssassinsJsonFile = @"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\assassins.json";
        private const string _pathToBeggarsJsonFile = @"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\beggars.json";
        private const string _pathToFoolsJsonFile = @"C:\Users\Віктор\Desktop\Valtech\OOPExercise\OOPExercise\OOPExercise\InputData\fools.json";


        static void Main(string[] args)
        {
            ConsoleViewer.ShowWelcomeWord();
            var name = ConsoleViewer.GetPlayerName();

            var scenario = new ScenarioCreator();
            var player = new Player(name);
            
            try
            {
                scenario.InitialiseAssassinsGuild(_pathToAssassinsJsonFile);
                scenario.InitialiseBeggarsGuild(_pathToBeggarsJsonFile);
                scenario.InitialiseFoolsGuild(_pathToFoolsJsonFile);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }                     

            do
            {
                var meeting = scenario.CreateRandomGuildMeeting();
                
                ConsoleViewer.ShowMeetingWelcomeInfo(meeting);
                ConsoleViewer.ShowCurrentBudget(player.CurrentBudget);

                if (meeting.WelcomeMessage.ToLower().Contains("beer"))
                {
                    Console.WriteLine($"Sorry, {player.Name}. But I relly need only beer.");
                    ConsoleViewer.ShowMeetingResultInfo(scenario.Skip(player));
                }
                else
                {
                    ConsoleViewer.ShowChoose();
                    var choice = ConsoleViewer.GetChoice();

                    if (choice == 1)
                    {
                        if (meeting.ToString().ToLower().Contains("assassin"))
                        {                            
                            var fee = ConsoleViewer.GetEnteredFee(player);
                            scenario.UseEnteredFee(fee);
                        }
                        ConsoleViewer.ShowMeetingResultInfo(scenario.Accept(player));
                    }
                    if (choice == 2)
                        ConsoleViewer.ShowMeetingResultInfo(scenario.Skip(player));
                }

            } while(player.IsAlive);

            ConsoleViewer.ShowScore(player.ToString());

            Console.ReadKey();
        }
    }
}
