using System;
using System.Collections.Generic;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    internal class FoolsGuild : Guild
    {
        private FoolNpc _activeNpc;

        protected internal override string WelcomeMessage
        {
            get => $"Hi! I'm your friend in Ankh-Morpork!" +
                $"\nAccept to earn some money in this city. If you skip, you get nothing.";
        }

        protected internal override ConsoleColor GuildColor => ConsoleColor.Yellow;

        protected internal void CreateNpc()
        {
            var fool = new FoolNpc();
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
            else
                throw new ArgumentException("The same fool is already exist.");
        }

        protected internal void CreateNpc(string name)
        {
            var fool = new FoolNpc(name);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
            else
                throw new ArgumentException("The same fool is already exist.");
        }

        protected internal void CreateNpc(FoolsPractice practice)
        {
            var fool = new FoolNpc(practice);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
            else
                throw new ArgumentException("The same fool is already exist.");
        }

        protected internal void CreateNpc(string name, FoolsPractice practice)
        {
            var fool = new FoolNpc(name, practice);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
            else
                throw new ArgumentException("The same fool is already exist.");
        }

        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (FoolNpc fool in npcs)
            {
                if (!ExistsNpc(fool))
                    Npcs.Add(fool);
                else
                    throw new ArgumentException("The same fool is already exist.");
            }
        }

        protected internal void CreateNpcs(JArray npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The array of NPCs cannot be null.");

            foreach (JObject npc in npcs.Children<JObject>())
            {
                var practice = (FoolsPractice)Enum.Parse(typeof(FoolsPractice), npc.GetValue("Practice").ToString());
                var fool = new FoolNpc(npc.GetValue("Name").ToString(), practice);
                if (!ExistsNpc(fool))
                    Npcs.Add(fool);
                else
                    throw new ArgumentException("The same fool is already exist.");
            }
        }

        private bool ExistsNpc(Npc npc)
        {
            var fool = npc as FoolNpc;
            if (fool is not null)
            {
                foreach (FoolNpc foolNpc in Npcs)
                    if (foolNpc.Equals(fool))
                        return true;
            }
            return false;
        }

        protected internal override Npc GetNpc()
        {
            if (Npcs.Equals(null) || Npcs.Count.Equals(0))
                throw new ArgumentNullException("No one Fool was created.");

            _activeNpc = Npcs[new Random().Next(0, Npcs.Count)] as FoolNpc;
            return _activeNpc;
        }

        protected internal override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            player.EarnMoney(_activeNpc.Bonus);
            return $"Our congratulations! You earned some money.\nBut remember, you have to pretend to be a fool, as your friend {((FoolNpc)GetNpc()).FullPracticeName}.";
        }

        protected internal override string LoseGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            return $"That's strange. You lost easy money.\n*These players, sometimes, are so illogical.";
        }

        public override string ToString() => $"Fools' Guild";
    }
}
