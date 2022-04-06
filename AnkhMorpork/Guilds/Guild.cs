using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

        protected internal virtual Npc GetNpc()
        {
            if (!Npcs.Equals(null) && Npcs.Count > 0)
                return Npcs[0];
            else
                throw new ArgumentNullException("No one NPC was created.");
        }

        protected internal abstract void PlayGame(Player player);

        protected internal virtual void LoseGame(Player player)
        {
            player.ToDie();
        }
    }
}
