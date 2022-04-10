using System;
using System.Collections.Generic;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using AnkhMorpork.Builders;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    public class BeggarsGuild : Guild
    {
        private BeggarNpc _activeNpc;

        public override string WelcomeMessage
        {
            get => $"Please, donate some money.. Save your soul." +
                $"\nAccept to donate a fix sum of money and make a good deed." +
                $"\nOr if you skip us, you will chase you to death..";
        }

        public override ConsoleColor GuildColor => ConsoleColor.DarkGreen;

        public void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (BeggarNpc beggar in npcs)
            {
                if (Npcs.Contains(beggar))
                    throw new ArgumentException("The same beggar is already exist.");
                else
                    Npcs.Add(beggar);
            }              
        }

        public void CreateNpcs(JArray npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            var beggar = new BeggarBuilder();

            foreach (JObject npc in npcs.Children<JObject>())
            {
                beggar.Reset();
                beggar.SetName(npc.GetValue("Name").ToString());
                var practice = (BeggarsPractice)Enum.Parse(typeof(BeggarsPractice), npc.GetValue("Practice").ToString());
                beggar.SetPractice(practice);

                if (Npcs.Contains(beggar.GetNpc()))
                    throw new ArgumentException("The same beggar is already exist.");
                else
                    Npcs.Add(beggar.GetNpc());
                
            }
        }

        protected internal override Npc GetActiveNpc()
        {
            _activeNpc = (BeggarNpc)base.GetActiveNpc();
            return _activeNpc;
        }

        public override string PlayGame(Player player)
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
                return player.ToDie() + $" Unfortunately, you didn't have enough money to donate. And {_activeNpc.Name} chased you to death.";
        }

        public override string LoseGame(Player player)
        {
            return base.LoseGame(player) + $" Unfortunately, beggars don't forgive deeds like this.";
        }

        public override string ToString() => $"Beggars' Guild";
    }
}
