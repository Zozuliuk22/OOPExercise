using System;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;

namespace AnkhMorpork
{
    public class Meeting
    {     
        public Guild Guild { get; private set; }

        public Npc Npc { get; private set; }

        internal Meeting(Guild guild)
        {
            if(guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            Guild = guild;
        }

        internal Meeting(Guild guild, Npc npc)
        {
            if (guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            Guild = guild;

            if(npc is null)
                throw new ArgumentNullException("The NPC value cannot be null.");

            Npc = npc;
        }

        public override string ToString()
        {
            return $"You have a meeting with the {Guild}.";
        }
    }
}
