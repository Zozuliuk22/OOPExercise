using System;
using System.Collections.Generic;
using System.Linq;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using AnkhMorpork.Guilds;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork.Guilds
{
    internal class FoolsGuild : Guild
    {
        private FoolNpc _activeNpc;

        protected internal void CreateNpc()
        {
            var fool = new FoolNpc();
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
        }

        protected internal void CreateNpc(string name)
        {
            var fool = new FoolNpc(name);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
        }
        protected internal void CreateNpc(FoolsPractice practice)
        {
            var fool = new FoolNpc(practice);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
        }

        protected internal void CreateNpc(string name, FoolsPractice practice)
        {
            var fool = new FoolNpc(name, practice);
            if (!ExistsNpc(fool))
                Npcs.Add(fool);
        }

        protected internal void CreateNpcs(IEnumerable<Npc> npcs)
        {
            foreach (FoolNpc fool in npcs)
            {
                if (!ExistsNpc(fool))
                    Npcs.Add(fool);
            }
        }

        protected internal bool ExistsNpc(Npc npc)
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
                throw new ArgumentException("No one Fool was created.");

            _activeNpc = Npcs[new Random().Next(0, Npcs.Count)] as FoolNpc;
            return _activeNpc;
        }

        protected internal override void PlayGame(Player player)
        {
            player.EarnMoney(_activeNpc.Bonus);
        }

        protected internal override void LoseGame(Player player)
        {
            
        }

        public override string ToString()
        {
            return $"Fools' Guild";
        }
    }
}
