using System;
using System.Collections.Generic;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    internal class BeggarsGuild : Guild
    {
        private BeggarNpc _activeNpc;

        protected internal override string WelcomeMessage
        {
            get => $"Please, donate some money.. Save your soul." +
                $"\nAccept to donate a fix sum of money and make a good deed." +
                $"\nOr if you skip us, you will chase you to death..";
        }

        protected internal override ConsoleColor GuildColor => ConsoleColor.DarkGreen;

        protected internal void CreateNpc()
        {
            var beggar = new BeggarNpc();
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
            else
                throw new ArgumentException("The same beggar is already exist.");
        }

        protected internal void CreateNpc(string name)
        {
            var beggar = new BeggarNpc(name);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
            else
                throw new ArgumentException("The same beggar is already exist.");
        }

        protected internal void CreateNpc(BeggarsPractice practice)
        {
            var beggar = new BeggarNpc(practice);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
            else
                throw new ArgumentException("The same beggar is already exist.");
        }

        protected internal void CreateNpc(string name, BeggarsPractice practice)
        {
            var beggar = new BeggarNpc(name, practice);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
            else
                throw new ArgumentException("The same beggar is already exist.");
        }

        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (BeggarNpc beggar in npcs)
            {
                if (!ExistsNpc(beggar))
                    Npcs.Add(beggar);
                else
                    throw new ArgumentException("The same beggar is already exist.");
            }              
        }

        protected internal void CreateNpcs(JArray npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The array of NPCs cannot be null.");

            foreach (JObject npc in npcs.Children<JObject>())
            {
                var practice = (BeggarsPractice)Enum.Parse(typeof(BeggarsPractice), npc.GetValue("Practice").ToString());
                var beggar = new BeggarNpc(npc.GetValue("Name").ToString(), practice);
                if (!ExistsNpc(beggar))
                    Npcs.Add(beggar);
                else
                    throw new ArgumentException("The same beggar is already exist.");
            }
        }

        private bool ExistsNpc(Npc npc)
        {
            var beggar = npc as BeggarNpc;
            if(beggar is not null)
            {
                foreach(BeggarNpc beggarNpc in Npcs)
                    if(beggarNpc.Equals(beggar))
                        return true;
            }
            return false;
        }

        protected internal override Npc GetNpc()
        {
            if (Npcs.Equals(null) || Npcs.Count.Equals(0))
                throw new ArgumentNullException("No one Beggar was created.");

            _activeNpc = Npcs[new Random().Next(0, Npcs.Count)] as BeggarNpc;
            return _activeNpc;
        }

        protected internal override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (_activeNpc.Practice.Equals(BeggarsPractice.BeerNeeders))
                return player.ToDie() + "Lack of beer is sometimes fatal.";
            else if (player.CurrentBudget >= _activeNpc.Fee)
            {
                player.LoseMoney(_activeNpc.Fee);
                return $"You donated some money. Good deeds come back like a boomerang. Therefore, live on.";
            }
            else
                return player.ToDie() + $" Unfortunately, you didn't have enough money to donate. And {GetNpc()} chased you to death.";
        }

        protected internal override string LoseGame(Player player)
        {
            return base.LoseGame(player) + $" Unfortunately, beggars don't forgive deeds like this.";
        }

        public override string ToString() => $"Beggars' Guild";
    }
}
