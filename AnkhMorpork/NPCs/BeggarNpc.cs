using System;
using AnkhMorpork.Enums;

namespace AnkhMorpork.NPCs
{   
    internal class BeggarNpc : Npc
    {
        protected internal BeggarsPractice Practice { get; private set; }

        protected internal decimal Fee { get; private set; }

        protected internal string FullPracticeName { get; private set; }

        internal BeggarNpc() : base()
        {
            SetRandomPractice();
            SetPracticeInfo();
        }

        internal BeggarNpc(string name) : base(name)
        {
            SetRandomPractice();
            SetPracticeInfo();
        }

        internal BeggarNpc(BeggarsPractice practice) : base()
        {
            Practice = practice;
            SetPracticeInfo();
        }

        internal BeggarNpc(string name, BeggarsPractice practice) : base(name)
        {
            Practice = practice;
            SetPracticeInfo();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BeggarNpc);
        }

        private bool Equals(BeggarNpc other)
        {
            if(other is null)
                return false;
            else
            {
                if(other.Name.Equals(Name) && other.Practice.Equals(Practice))
                    return true;
                return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Practice);
        }

        public override string ToString() => $"{Name} from {FullPracticeName}";

        private void SetRandomPractice()
        {
            var practies = Enum.GetValues(typeof(BeggarsPractice));
            Practice = (BeggarsPractice)practies.GetValue(new Random().Next(0, practies.Length));           
        }

        private void SetPracticeInfo()
        {
            FullPracticeName = Constants.BeggarsPracticeInfo[Practice].Item1;
            Fee = Constants.BeggarsPracticeInfo[Practice].Item2;
        }
    }
}
