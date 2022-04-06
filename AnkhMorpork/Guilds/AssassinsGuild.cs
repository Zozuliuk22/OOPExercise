using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnkhMorpork.NPCs;

namespace AnkhMorpork.Guilds
{
    internal class AssassinsGuild : Guild
    {
        private AssassinNpc _activeNpc;
        private decimal _enteredFee;

        protected internal void CreateNpc()
        {
            var assassin = new AssassinNpc();
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
        }

        protected internal void CreateNpc(string name)
        {
            var assassin = new AssassinNpc(name);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
        }

        protected internal void CreateNpc(int minReward, int maxReward)
        {
            var assassin = new AssassinNpc(minReward, maxReward);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
        }

        protected internal void CreateNpc(string name, int minReward, int maxReward)
        {
            var assassin = new AssassinNpc(name, minReward, maxReward);
            if (!ExistsNpc(assassin))
                Npcs.Add(assassin);
        }

        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            foreach (AssassinNpc assassin in npcs)
            {
                if (!ExistsNpc(assassin))
                    Npcs.Add(assassin);
            }
        }

        protected internal bool ExistsNpc(Npc npc)
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
            _activeNpc = null;

            if(fee > 0)
            {
                _activeNpc = Npcs.OfType<AssassinNpc>()
                           .Where(v => v.IsOccupied.Equals(false))
                           .Where(v => v.MinReward <= fee && v.MaxReward >= fee)
                           .OrderBy(v => v.Name)
                           .FirstOrDefault(); 
            }

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

        protected internal override void PlayGame(Player player)
        {
            if (_activeNpc is not null)
            {
                if (player.CurrentBudget >= _enteredFee)
                    player.LoseMoney(_enteredFee);                    
            }

            player.ToDie();
        }

        public override string ToString()
        {
            return $"Assassins' Guild";
        }
    }
}
