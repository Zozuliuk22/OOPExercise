using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork.NPCs
{
    internal class AssassinNpc : Npc
    {
        private DateTime _startingDataOccupied = DateTime.Now.AddSeconds(-60);

        protected internal int MinReward { get; private set; }

        protected internal int MaxReward { get; private set; }

        protected internal bool IsOccupied 
        { 
            get
            {
                if (DateTime.Now.Subtract(_startingDataOccupied).TotalSeconds <= 30)
                    return true;
                else
                    return false;
            }
        }

        internal AssassinNpc() : base()
        {
            SetRandomRewandRange();
        }

        internal AssassinNpc(string name) : base(name)
        {
            SetRandomRewandRange();
        }

        internal AssassinNpc(int minReward, int maxReward)
        {
            if (minReward > 0)
                MinReward = minReward;
            else
                throw new ArgumentException("Minimum reward limit cannot be less or equal than zero.");

            if (maxReward > minReward)
                MaxReward = maxReward;
            else
                throw new ArgumentException("Maximum reward limit cannot be less or equal than minimum reward limit.");
        }

        internal AssassinNpc(string name, int minReward, int maxReward) : base(name)
        {
            if (minReward > 0)
                MinReward = minReward;
            else
                throw new ArgumentException("Minimum reward limit cannot be less or equal than zero.");

            if (maxReward > minReward)
                MaxReward = maxReward;
            else
                throw new ArgumentException("Maximum reward limit cannot be less or equal than minimum reward limit.");
        }

        private void SetRandomRewandRange()
        {
            MinReward = new Random().Next(0, 15);
            MaxReward = MinReward + 10;
        }

        internal void TakeContract()
        {
            if(!IsOccupied)
                _startingDataOccupied = DateTime.Now;
        }
    }
}
