using System;

namespace AnkhMorpork.NPCs
{
    internal class AssassinNpc : Npc
    {
        private DateTime _startingDataOccupied = DateTime.Now.AddSeconds(-300);

        protected internal decimal MinReward { get; private set; }

        protected internal decimal MaxReward { get; private set; }

        protected internal bool IsOccupied 
        { 
            get
            {
                if (DateTime.Now.Subtract(_startingDataOccupied).TotalSeconds <= 180)
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

        internal AssassinNpc(decimal minReward, decimal maxReward)
        {
           SetRewawdRange(minReward, maxReward);
        }

        internal AssassinNpc(string name, decimal minReward, decimal maxReward) : base(name)
        {
            SetRewawdRange(minReward, maxReward);
        }        

        internal void TakeContract()
        {
            if (!IsOccupied)
                _startingDataOccupied = DateTime.Now;
            else
                throw new ArgumentException($"Assassin {Name} is already occupied.");
        }

        private void SetRandomRewandRange()
        {
            MinReward = new Random().Next(1, 16);
            MaxReward = MinReward + 10;
        }

        private void SetRewawdRange(decimal minReward, decimal maxReward)
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

        public override bool Equals(object obj)
        {
            return Equals(obj as AssassinNpc);
        }

        private bool Equals(AssassinNpc other)
        {
            if (other is null)
                return false;
            else
            {
                if (other.Name.Equals(Name) 
                    && other.MinReward.Equals(MinReward) 
                    && other.MaxReward.Equals(MaxReward))
                    return true;
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, MinReward, MaxReward);
        }
    }
}
