using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    internal abstract class Npc
    {
        protected internal string Name { get; protected set; }

        protected internal Npc()
        {
            Name = "Unknown";
        }

        protected internal Npc(string name)
        {
            Name = name;
        }
    }
}
