using System;
using System.Collections.Generic;
using System.Linq;
using AnkhMorpork.NPCs;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    internal class AssassinsGuild : Guild
    {
        private AssassinNpc _activeNpc;
        private decimal _enteredFee;

        protected internal override string WelcomeMessage
        {
            get => "Oops... Someone signed a contract to kill you." +
                "\nIf you wanna survive and go on to enjoy your life, you must pay." +
                "\nAccept to cooperate with us, enter a random fee and we try to find an assassin to kill your enemy." +
                "\nIf you skip, so... Our member will have fulfilled a contract out on you.";
        }

        protected internal override ConsoleColor GuildColor => ConsoleColor.DarkMagenta;

        protected internal void CreateNpc()
        {
            var assassin = new AssassinNpc();
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
            else
                throw new ArgumentException("The same assassin is already exist.");
        }

        protected internal void CreateNpc(string name)
        {
            var assassin = new AssassinNpc(name);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
            else
                throw new ArgumentException("The same assassin is already exist.");
        }

        protected internal void CreateNpc(int minReward, int maxReward)
        {
            var assassin = new AssassinNpc(minReward, maxReward);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
            else
                throw new ArgumentException("The same assassin is already exist.");
        }

        protected internal void CreateNpc(string name, int minReward, int maxReward)
        {
            var assassin = new AssassinNpc(name, minReward, maxReward);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
            else
                throw new ArgumentException("The same assassin is already exist.");
        }

        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (AssassinNpc assassin in npcs)
            {
                if (!ExistsNpc(assassin))
                    Npcs.Add(assassin);
                else
                    throw new ArgumentException("The same assassin is already exist.");
            }
        }

        protected internal void CreateNpcs(JArray npcs)
        {
            if(npcs is null)
                throw new ArgumentNullException("The array of NPCs cannot be null.");

            foreach (JObject npc in npcs.Children<JObject>())
            {
                var assassin = new AssassinNpc(npc.GetValue("Name").ToString());
                if (!ExistsNpc(assassin))
                    Npcs.Add(assassin);
                else
                    throw new ArgumentException("The same assassin is already exist.");
            }
        }

        private bool ExistsNpc(Npc npc)
        {
            var assassin = npc as AssassinNpc;
            if (assassin is not null)
            {
                foreach (AssassinNpc assassinNpc in Npcs)
                    if (assassinNpc.Equals(assassin))
                        return true;
            }
            return false;
        }

        protected internal bool CheckContract(decimal fee)
        {
            if(fee > 0)
            {
                _activeNpc = Npcs.OfType<AssassinNpc>()
                           .Where(v => v.IsOccupied.Equals(false))
                           .Where(v => v.MinReward <= fee && v.MaxReward >= fee)
                           .OrderBy(v => v.Name)
                           .FirstOrDefault();
            }
            else
                throw new ArgumentException("The entered fee must be bigger than zero.");

            if (_activeNpc is null)
                return false;

            _enteredFee = fee;
            return true;
        }

        protected internal override Npc GetNpc()
        {
            if (Npcs.Equals(null) || Npcs.Count.Equals(0))
                throw new ArgumentNullException("No one Assassin was created.");
            else if (_activeNpc is null)
                throw new ArgumentNullException("All assassins are occupied.");
            return _activeNpc;
        }    

        protected internal override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (_activeNpc is null)
                return player.ToDie() + " Sorry, but no one could take your contract.";
            else
            {
                _activeNpc.TakeContract();
                player.LoseMoney(_enteredFee);
                return $"You are lucky! Assassin {GetNpc()} went to fulfill the contract.";
            }
        }

        protected internal override string LoseGame(Player player)
        {
            return base.LoseGame(player) + " That's just an assassin's job.";
        }

        public override string ToString() => $"Assassins' Guild";
    }
}
