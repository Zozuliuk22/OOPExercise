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

        protected internal override string GetNpc()
        {
            return "The member of Thieves' Guild is unknown.";
        }

        protected internal override void PlayGame(Player player)
        {
            player.LoseMoney(DefaultFee);
        }

        public override string ToString()
        {
            return "Thieves' Guild";
        }        
    }
}
