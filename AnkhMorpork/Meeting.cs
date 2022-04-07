using System;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;

namespace AnkhMorpork
{
    public class Meeting
    {
        private Guild _guild;
        private Npc _npc;        

        internal Guild Guild => _guild;

        internal Npc Npc => _npc;

        public string WelcomeMessage { get; private set; }

        public ConsoleColor MessageColor
        {
            get => _guild.GuildColor;
        }

        internal Meeting(Guild guild)
        {
            if(guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            _guild = guild;

            SetWelcomeMessage();
        }

        internal Meeting(Guild guild, Npc npc)
        {
            if (guild is null)
                throw new ArgumentNullException("The guild value cannot be null.");

            _guild = guild;

            if(npc is null)
                throw new ArgumentNullException("The NPC value cannot be null.");

            _npc = npc;

            SetWelcomeMessage();
        }

        public override string ToString()
        {
            return $"You have a meeting with the {Guild}.";
        }

        private void SetWelcomeMessage()
        {
            if(Guild is AssassinsGuild || Guild is ThievesGuild)
                WelcomeMessage = Guild.WelcomeMessage;

            if (Guild is BeggarsGuild)
            {
                if(((BeggarNpc)Npc).Fee >= 1)
                    WelcomeMessage = _guild.WelcomeMessage + "\n\nI'm " + Npc + $". And I just need {((BeggarNpc)Npc).Fee} AM$";
                else
                    WelcomeMessage = _guild.WelcomeMessage + "\n\nI'm " + Npc + $". And I just need {Math.Truncate(((BeggarNpc)Npc).Fee*100)} pence";
            }

            if (Guild is FoolsGuild)
            {
                if (((FoolNpc)Npc).Bonus >= 1)
                    WelcomeMessage = _guild.WelcomeMessage + "\n\nI'm " + Npc + $". And I can give you {((FoolNpc)Npc).Bonus} AM$";
                else
                    WelcomeMessage = _guild.WelcomeMessage + "\n\nI'm " + Npc + $". And I can give you {Math.Truncate(((FoolNpc)Npc).Bonus*100)} pence";
            }

        }
    }
}
