using System;
using System.Collections.Generic;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using AnkhMorpork.Builders;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    public class FoolsGuild : Guild
    {
        private FoolNpc _activeNpc;

        public override string WelcomeMessage
        {
            get => $"Hi! I'm your friend in Ankh-Morpork!" +
                $"\nAccept to earn some money in this city. If you skip, you get nothing.";
        }

        public override ConsoleColor GuildColor => ConsoleColor.Yellow;

        public void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (FoolNpc fool in npcs)
            {
                if (Npcs.Contains(fool))
                    throw new ArgumentException("The same fool is already exist.");
                else
                    Npcs.Add(fool);
            }
        }

        public void CreateNpcs(JArray npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            var fool = new FoolBuilder();

            foreach (JObject npc in npcs.Children<JObject>())
            {
                fool.Reset();
                fool.SetName(npc.GetValue("Name").ToString());
                var practice = (FoolsPractice)Enum.Parse(typeof(FoolsPractice), npc.GetValue("Practice").ToString());
                fool.SetPractice(practice);

                if (Npcs.Contains(fool.GetNpc()))
                    throw new ArgumentException("The same fool is already exist.");                
                else
                    Npcs.Add(fool.GetNpc());
            }
        }

        public override Npc GetActiveNpc()
        {
            _activeNpc = (FoolNpc)base.GetActiveNpc();
            return _activeNpc;
        }

        public override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            player.EarnMoney(_activeNpc.Bonus);
            return $"Our congratulations! You earned some money.\nBut remember, you have to pretend to be a fool, as your friend {_activeNpc.FullPracticeName}.";
        }

        public override string LoseGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            return $"That's strange. You lost easy money.\n*These players, sometimes, are so illogical.";
        }

        public override string ToString() => $"Fools' Guild";
    }
}
