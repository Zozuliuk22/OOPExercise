using System;
using AnkhMorpork.NPCs;

namespace AnkhMorpork.Builders
{
    public class AssassinBuilder : INpcBuilder<AssassinNpc>
    {
        private AssassinNpc _assassin;

        public AssassinBuilder() => _assassin = new AssassinNpc();

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The Assassin's name must consist of symbols.");
            _assassin.Name = name;
        }

        public void SetMinReward(decimal minReward)
        {
            if (minReward > 0)
                _assassin.MinReward = minReward;
            else
                throw new ArgumentException("Minimum reward limit cannot be less or equal than zero.");
        }

        public void SetMaxReward(decimal maxReward)
        {
            if (maxReward > _assassin.MinReward)
                _assassin.MaxReward = maxReward;
            else
                throw new ArgumentException("Maximum reward limit cannot be less or equal than minimum reward limit.");
        }

        public void Reset()
        {
            _assassin = new AssassinNpc();
        }

        public AssassinNpc GetNpc()
        {
            return _assassin;
        }

        public void SetRandomRewandRange()
        {
            _assassin.MinReward = new Random().Next(1, 16);
            _assassin.MaxReward = _assassin.MinReward + 10;
        }
    }
}
