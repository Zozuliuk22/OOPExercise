using System;

namespace AnkhMorpork.Guilds
{
    internal class ThievesGuild : Guild
    {
        private const int _maxNumberThefts = 6;
        private const decimal _defaultFee = 10;

        protected internal override string WelcomeMessage
        {
            get => "Hey! How's it going? Do you have something in your pockets?" +
                "\nAccept to pay off us and loose 10 AM$." +
                "\nIf you don't wanna pay, you will be died. Such rules...";
        }

        protected internal override ConsoleColor GuildColor => ConsoleColor.Blue;

        protected internal int MaxNumberThefts => _maxNumberThefts;

        protected internal decimal DefaultFee => _defaultFee;

        protected internal int CurrentNumberThefts { get; private set; } = 0;

        protected internal void AddTheft() => CurrentNumberThefts += 1;

        protected internal override string PlayGame(Player player)
        {
            if(player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");

            if (player.CurrentBudget >= DefaultFee)
            {
                player.LoseMoney(DefaultFee);
                return $"Unknown from the {ToString()} stole {DefaultFee} AM$ from you.";
            }                
            else
                return LoseGame(player) + $" Unfortunately, you didn't have enough money to pay off the {ToString()}.";
        }

        protected internal override string LoseGame(Player player)
        {
            return base.LoseGame(player) + " That's a result of thieves' codex.";
        }

        public override string ToString() => "Thieves' Guild";
    }
}
