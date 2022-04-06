using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnkhMorpork.Enums;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AnkhMorpork.NPCs
{   
    internal class BeggarNpc : Npc
    {
        protected internal BeggarsPractice Practice { get; private set; }

        protected internal decimal Fee { get; private set; }

        protected internal string FullPracticeName { get; private set; }

        internal BeggarNpc() : base()
        {
            SetDefaultPractice();
        }

        internal BeggarNpc(string name) : base(name)
        {
            SetDefaultPractice();
        }

        internal BeggarNpc(BeggarsPractice practice) : base()
        {
            Practice = practice;
            FullPracticeName = Constants.BeggarsPracticeInfo[Practice].Item1;
            Fee = Constants.BeggarsPracticeInfo[Practice].Item2;
        }

        internal BeggarNpc(string name, BeggarsPractice practice) : base(name)
        {
            Practice = practice;
            FullPracticeName = Constants.BeggarsPracticeInfo[Practice].Item1;
            Fee = Constants.BeggarsPracticeInfo[Practice].Item2;
        }

        private void SetDefaultPractice()
        {
            Practice = BeggarsPractice.Twitchers;
            FullPracticeName = Constants.BeggarsPracticeInfo[Practice].Item1;
            Fee = Constants.BeggarsPracticeInfo[Practice].Item2;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BeggarNpc);
        }

        public bool Equals(BeggarNpc beggar)
        {
            if(beggar is null)
                return false;
            else
            {
                if(beggar.Name.Equals(Name) 
                    && beggar.Practice.Equals(Practice))
                    return true;
                return false;
            }
        }

        public override string ToString()
        {
            return $"I'm {Name} and I'm one of {FullPracticeName}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Practice);
        }
    }
}
