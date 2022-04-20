using System;
using System.Collections.Generic;
using System.Linq;
using AnkhMorpork.NPCs;
using AnkhMorpork.Builders;
using Newtonsoft.Json.Linq;

namespace AnkhMorpork.Guilds
{
    public class AssassinsGuild : Guild
    {
        private AssassinNpc _activeNpc;
        private decimal _enteredFee;

        public override string WelcomeMessage
        {
            get => "Oops... Someone signed a contract to kill you." +
                "\nIf you wanna survive and go on to enjoy your life, you must pay." +
                "\nAccept to cooperate with us, enter a random fee and we try to find an assassin to kill your enemy." +
                "\nIf you skip, so... Our member will have fulfilled a contract out on you.";
        }

        public override ConsoleColor GuildColor => ConsoleColor.DarkMagenta;

        /// <summary>
        /// To add unique NPCs to the global list from the local list of npcs.
        /// </summary>
        /// <param name="npcs">The local list of npcs.</param>
        /// <exception cref="ArgumentNullException">The collection of NPCs cannot be null.</exception>
        /// <exception cref="ArgumentException">The same fool is already exist.</exception>
        public void CreateNpcs(IEnumerable<Npc> npcs)
        {
            if (npcs is null)
                throw new ArgumentNullException("The collection of NPCs cannot be null.");

            foreach (AssassinNpc assassin in npcs)
            {
                if (Npcs.Contains(assassin))
                    throw new ArgumentException("The same assassin is already exist.");
                else
                    Npcs.Add(assassin);
            }
        }

        /// <summary>
        /// To add unique NPCs to the global list from the local Json array of npcs.
        /// </summary>
        /// <param name="npcs">The local Json array of npcs.</param>
        /// <exception cref="ArgumentNullException">The collection of NPCs cannot be null.</exception>
        /// <exception cref="ArgumentException">The same fool is already exist.</exception>
        public void CreateNpcs(JArray npcs)
        {
            if(npcs is null)
                throw new ArgumentNullException("The array of NPCs cannot be null.");

            var assassin = new AssassinBuilder();

            foreach (JObject npc in npcs.Children<JObject>())
            {
                assassin.Reset();
                assassin.SetName(npc.GetValue("Name").ToString());
                assassin.SetRandomRewandRange();

                if (Npcs.Contains(assassin.GetNpc()))
                    throw new ArgumentException("The same assassin is already exist.");
                else
                    Npcs.Add(assassin.GetNpc());
                
            }
        }

        /// <summary>
        /// Try to find the ready assassin who can take a contract by the entered fee.
        /// </summary>
        /// <param name="fee">The entered fee by a player.</param>
        /// <returns>True if there is an assassin who is ready to take the contract, otherwise False.</returns>
        /// <exception cref="ArgumentException">The entered fee must be bigger than zero.</exception>
        public bool CheckContract(decimal fee)
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

        public override Npc GetActiveNpc()
        {
            if (_activeNpc.IsOccupied)
                throw new Exception("Before this, player must enter fee and check contract.");
            return _activeNpc;
        }    

        public override string PlayGame(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (_activeNpc is null)
                return player.ToDie() + " Sorry, but no one could take your contract.";
            else
            {
                _activeNpc.TakeContract();
                player.LoseMoney(_enteredFee);
                return $"You are lucky! Assassin {_activeNpc} went to fulfill the contract.";
            }
        }

        public override string LoseGame(Player player)
        {
            return base.LoseGame(player) + " That's just an assassin's job.";
        }

        public override string ToString() => $"Assassins' Guild";
    }
}
