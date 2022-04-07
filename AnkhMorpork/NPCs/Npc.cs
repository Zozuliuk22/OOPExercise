using System;

namespace AnkhMorpork
{
    internal abstract class Npc
    {
        protected internal string Name { get; private set; }

        protected Npc() => Name = "Unknown";

        protected Npc(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException($"The NPC's name must consist of symbols.");
            Name = name;
        }

        public override string ToString() => Name ?? "Unknown";
    }
}
