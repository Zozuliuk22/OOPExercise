using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            SetDefaultPractice();
        }

        internal FoolNpc(string name) : base(name)
        {
            SetDefaultPractice();
        }

        internal FoolNpc(FoolsPractice practice) : base()
        {
            Practice = practice;
            FullPracticeName = Constants.FoolsPracticeInfo[Practice].Item1;
            Bonus = Constants.FoolsPracticeInfo[Practice].Item2;
        }

        internal FoolNpc(string name, FoolsPractice practice) : base(name)
        {
            Practice = practice;
            FullPracticeName = Constants.FoolsPracticeInfo[Practice].Item1;
            Bonus = Constants.FoolsPracticeInfo[Practice].Item2;
        }

        private void SetDefaultPractice()
        {
            Practice = FoolsPractice.Muggins;
            FullPracticeName = Constants.FoolsPracticeInfo[Practice].Item1;
            Bonus = Constants.FoolsPracticeInfo[Practice].Item2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FoolNpc);
        }

        public bool Equals(FoolNpc fool)
        {
            if (fool is null)
                return false;
            else
            {
                if (fool.Name.Equals(Name)
                    && fool.Practice.Equals(Practice))
                    return true;
                return false;
            }
        }

        public override string ToString()
        {
            return $"I'm {Name} and I'm one of {FullPracticeName} practice." +
                $"\nIf you wanna earn some money, I can pay you {Math.Truncate(Bonus)} AM$ {String.Format("{0 : 00}", Math.Truncate(Bonus * 100 % 100))} pence." +
                $"\nBut you must has to pretend to be a fool.";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Practice);
        }
    }
}
