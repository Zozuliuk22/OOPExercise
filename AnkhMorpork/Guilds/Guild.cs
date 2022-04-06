using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    internal abstract class Guild
    {
        protected List<Npc> Npcs;

        protected internal Guild()
        {
            Npcs = new List<Npc>();
        }

        protected internal virtual void CreateNpc()
        {
            throw new NotImplementedException();
        }

        protected internal virtual void CreateNpc(string name)
        {
            throw new NotImplementedException();
        }

        protected internal virtual void CreateNpcs(IEnumerable<Npc> npcs)
        {
            throw new NotImplementedException();
        }
        
        protected virtual bool ExistsNpc(Npc npc)
        {
            throw new NotImplementedException();
        }

        protected internal abstract string GetNpc();

        protected internal abstract void PlayGame(Player player);

        protected internal virtual void LoseGame(Player player)
        {
            player.ToDie();
        }
    }
}
