﻿using System;

namespace AnkhMorpork
{
    public class Player
    {
        private const decimal _startBudget = 100;
        private int _score = 0;

        public string Name { get; set; }

        public int Score => _score;

        public bool IsAlive { get; private set; }

        public decimal CurrentBudget { get; private set; }

        public Player(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The Player's name must consist of symbols.");

            Name = name;
            CurrentBudget = _startBudget;
            IsAlive = true;
        }

        public void EarnMoney(decimal bonus)
        {
            if (bonus < 0)
                throw new ArgumentException("Bonus must be bigger or equal then zero.");
            CurrentBudget += bonus;
            _score += 1;            
        }

        public void LoseMoney(decimal fee)
        {
            if (fee < 0 || fee > CurrentBudget)
                throw new ArgumentException("Fee must be bigger or equal then zero and less or equal than current budget.");
            CurrentBudget -= fee;
            _score += 1;
        }

        public string ToDie()
        {
            IsAlive = false;
            return "You were killed!";
        }

        public override string ToString() => $"Player {Name} have survived {Score} meetings.";
    }
}
