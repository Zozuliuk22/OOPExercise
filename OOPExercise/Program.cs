using System;
using AnkhMorpork;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;

namespace OOPExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleViewer.ShowWelcomeWord();
            var name = ConsoleViewer.GetPlayerName();

            var scenario = new ScenarioCreator();
            var player = new Player(name);
            
            try
            {
                scenario.InitialiseAllGuilds();
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

                if (meeting.Guild is BeggarsGuild && ((BeggarNpc)meeting.Npc).Practice.Equals(BeggarsPractice.BeerNeeders))
                {
                    Console.WriteLine(meeting.Npc.ToString());
                    ConsoleViewer.ShowMeetingResultInfo(scenario.Skip(player));
                }
                else
                {
                    ConsoleViewer.ShowChoice();
                    var choice = ConsoleViewer.GetChoice();

                    if (choice == 1)
                    {
                        if (meeting.Guild is AssassinsGuild)
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

            ConsoleViewer.ShowScore(player);

            Console.ReadKey();
        }
    }
}
