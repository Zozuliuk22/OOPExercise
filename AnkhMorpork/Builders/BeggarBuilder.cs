using System;
using AnkhMorpork.NPCs;
using AnkhMorpork.Enums;
using AnkhMorpork.Constants;

namespace AnkhMorpork.Builders
{
    public class BeggarBuilder : INpcBuilder<BeggarNpc>
    {
        private BeggarNpc _beggar;

        public BeggarNpc GetNpc()
        {
            return _beggar;
        }

        public void Reset()
        {
            _beggar = new BeggarNpc();
        }

        public void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The Beggar's name must consist of symbols.");
            _beggar.Name = name;
        }

        public void SetPractice(BeggarsPractice practice)
        {
            _beggar.Practice = practice;
            SetPracticeInfo();
        }

        public void SetRandomPractice()
        {
            var practies = Enum.GetValues(typeof(BeggarsPractice));
            _beggar.Practice = (BeggarsPractice)practies.GetValue(new Random().Next(0, practies.Length));
            SetPracticeInfo();
        }

        private void SetPracticeInfo()
        {
            _beggar.FullPracticeName = PracticesInfo.BeggarsPracticeInfo[_beggar.Practice].Item1;
            _beggar.Fee = PracticesInfo.BeggarsPracticeInfo[_beggar.Practice].Item2;
        }
    }
}
