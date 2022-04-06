using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    internal class ThievesGuild : Guild
    {
        private const int _maxNumberThefts = 6;
        private const decimal _defaultFee = 10;

        protected internal int MaxNumberThefts 
        { 
            get { return _maxNumberThefts; } 
        }

        protected internal decimal DefaultFee 
        { 
            get { return _defaultFee; } 
        }

        protected internal int CurrentNumberThefts { get; private set; } = 0;

        protected internal void AddTheft()
        {
            CurrentNumberThefts += 1;            
        }       

        protected internal override void PlayGame(Player player)
        {
            if (player.CurrentBudget >= DefaultFee)
                player.LoseMoney(DefaultFee);
            else
                player.ToDie();
        }

        public override string ToString()
        {
            return "Thieves' Guild";
        }        
    }
}
