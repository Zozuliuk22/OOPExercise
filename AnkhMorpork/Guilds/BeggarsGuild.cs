using System;
using System.Collections.Generic;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork.Guilds
{
    internal class BeggarsGuild : Guild
    {
        private BeggarNpc _activeNpc;

        protected internal void CreateNpc()
        {
            var beggar = new BeggarNpc();
            if(!ExistsNpc(beggar))
                Npcs.Add(beggar);
        }

        protected internal void CreateNpc(string name)
        {
            var beggar = new BeggarNpc(name);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
        }
        protected internal void CreateNpc(BeggarsPractice practice)
        {
            var beggar = new BeggarNpc(practice);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
        }

        protected internal void CreateNpc(string name, BeggarsPractice practice)
        {
            var beggar = new BeggarNpc(name, practice);
            if (!ExistsNpc(beggar))
                Npcs.Add(beggar);
        }


        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            foreach (BeggarNpc beggar in npcs)
            {
                if (!ExistsNpc(beggar))
                    Npcs.Add(beggar);
            }              
        }

        protected internal bool ExistsNpc(Npc npc)
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
                throw new ArgumentException("No one Beggar was created.");

            _activeNpc = Npcs[new Random().Next(0, Npcs.Count)] as BeggarNpc;
            return _activeNpc;
        }

        protected internal override void PlayGame(Player player)
        {
            if (_activeNpc.Practice.Equals(BeggarsPractice.BeerNeeders))
                player.ToDie();
            else if (player.CurrentBudget >= _activeNpc.Fee)
                player.LoseMoney(_activeNpc.Fee);
            else
                player.ToDie();
        }

        public override string ToString()
        {
            return $"Beggars' Guild";
        }
    }
}
