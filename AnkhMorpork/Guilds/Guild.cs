using System;
using System.Collections.Generic;

namespace AnkhMorpork
{
    public abstract class Guild
    {
        protected List<Npc> Npcs;

        public Guild() => Npcs = new List<Npc>();

        public virtual string WelcomeMessage => String.Empty;

        public virtual ConsoleColor GuildColor => ConsoleColor.White;

        /// <summary>
        /// Get an active NPC who may play during the current meeting.
        /// </summary>
        /// <returns>The NPC object.</returns>
        /// <exception cref="ArgumentNullException">The list of NPCs is empty and it's not possible to get a NPC.</exception>
        public virtual Npc GetActiveNpc()
        {
            if (!Npcs.Equals(null) && Npcs.Count > 0)
                return Npcs[new Random().Next(0, Npcs.Count)];
            else
                throw new ArgumentNullException("No one NPC was created.");
        }

        /// <summary>
        /// To play a game with the Guild and get text-result of this game.
        /// </summary>
        /// <param name="player">The Player object.</param>
        /// <returns>The result of a game with the guild as a string.</returns>
        public abstract string PlayGame(Player player);

        /// <summary>
        /// To lose a game with the Guild and get text-result of losing.
        /// </summary>
        /// <param name="player">The Player object.</param>
        /// <returns>The result of losing the game as a string.</returns>
        /// <exception cref="ArgumentNullException">The player value cannot be null.</exception>
        public virtual string LoseGame(Player player)
        {
            if(player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            return player.ToDie();
        }
    }
}
