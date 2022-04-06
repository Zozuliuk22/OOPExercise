using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    internal class Meeting
    {
        private Guild _guild;
        private Npc _npc;

        internal Guild Guild { get { return _guild; } }

        internal Npc Npc { get { return _npc; } }

        internal Meeting(Guild guild)
        {
            _guild = guild;            
        }

        internal Meeting(Guild guild, Npc npc)
        {
            _guild = guild;
            _npc = npc;
        }

        public override string ToString()
        {
            return $"You have a meeting with the {Guild}.";
        }
    }
}
