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
        private bool _isAlive = true;
        private int _score = 0;

        public string Name { get; set; }

        public int Score { get { return _score; } }

        public bool IsAlive 
        {
            get 
            { 
                if(CurrentBudget <= 0)
                    _isAlive = false;
                return _isAlive;
            }
        }

        public decimal CurrentBudget { get; private set; }

        public Player(string name)
        {
            Name = name;
            CurrentBudget = _startBudget;
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
            _isAlive = false;
        }

        public override string ToString()
        {
            return $"Player {Name} have survived {Score} meetings.";
        }
    }
}
