using System;
using AnkhMorpork.Enums;

namespace AnkhMorpork.NPCs
{
    internal class FoolNpc : Npc
    {
        protected internal FoolsPractice Practice { get; private set; }

        protected internal decimal Bonus { get; private set; }

        protected internal string FullPracticeName { get; private set; }

        internal FoolNpc() : base()
        {
            SetRandomPractice();
            SetPracticeInfo();
        }

        internal FoolNpc(string name) : base(name)
        {
            SetRandomPractice();
            SetPracticeInfo();
        }

        internal FoolNpc(FoolsPractice practice) : base()
        {
            Practice = practice;
            SetPracticeInfo();
        }

        internal FoolNpc(string name, FoolsPractice practice) : base(name)
        {

            Practice = practice;
            SetPracticeInfo();
        }        

        public override bool Equals(object obj)
        {
            return Equals(obj as FoolNpc);
        }

        private bool Equals(FoolNpc other)
        {
            if (other is null)
                return false;
            else
            {
                if (other.Name.Equals(Name)
                    && other.Practice.Equals(Practice))
                    return true;
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Practice);
        }

        public override string ToString() => $"{Name} from {FullPracticeName}s";

        private void SetRandomPractice()
        {
            var practies = Enum.GetValues(typeof(FoolsPractice));
            Practice = (FoolsPractice)practies.GetValue(new Random().Next(0, practies.Length));            
        }

        private void SetPracticeInfo()
        {
            FullPracticeName = Constants.FoolsPracticeInfo[Practice].Item1;
            Bonus = Constants.FoolsPracticeInfo[Practice].Item2;
        }
    }
}
