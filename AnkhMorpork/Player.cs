using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    public class Player
    {
        private const decimal _startBudget = 100;
        private int _score = 0;

        public string Name { get; set; }

        public int Score { get { return _score; } }

        public bool IsAlive { get; private set; }

        public decimal CurrentBudget { get; private set; }

        public Player(string name)
        {
            Name = name;
            CurrentBudget = _startBudget;
            IsAlive = true;
        }

        public void EarnMoney(decimal bonus)
        {
            CurrentBudget += bonus;
            _score += 1;
        }

        public void LoseMoney(decimal fee)
        {
            CurrentBudget -= fee;
            _score += 1;
        }

        public void ToDie()
        {
            IsAlive = false;
        }

        public override string ToString()
        {
            return $"Player {Name} have survived {Score} meetings.";
        }
    }
}
