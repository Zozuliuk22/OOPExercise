using System;

namespace AnkhMorpork
{
    public abstract class Npc
    {
        public string Name { get; set; }

        protected Npc() => Name = "Unknown";

        protected Npc(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException($"The NPC's name must consist of symbols.");

            Name = name;
        }

        public override string ToString() => Name;
    }
}
