using System;
using System.Collections.Generic;

namespace AnkhMorpork
{
    internal abstract class Guild
    {
        protected List<Npc> Npcs;

        protected Guild() => Npcs = new List<Npc>();

        protected internal virtual string WelcomeMessage => String.Empty;

        protected internal virtual ConsoleColor GuildColor => ConsoleColor.White;

        protected internal virtual Npc GetNpc()
        {
            if (!Npcs.Equals(null) && Npcs.Count > 0)
                return Npcs[0];
            else
                throw new ArgumentNullException("No one NPC was created.");
        }

        protected internal abstract string PlayGame(Player player);

        protected internal virtual string LoseGame(Player player)
        {
            if(player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");
            return player.ToDie();
        }
    }
}
