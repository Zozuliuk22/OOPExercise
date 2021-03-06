using System;
using AnkhMorpork.NPCs;
using AnkhMorpork.Constants;
using AnkhMorpork.Enums;

namespace AnkhMorpork.Builders
{
    public class FoolBuilder : INpcBuilder<FoolNpc>
    {
        private FoolNpc _fool;        

        public void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The Fool's name must consist of symbols.");
            _fool.Name = name;
        }

        /// <summary>
        /// Set the practice of fools' practices.
        /// </summary>
        /// <param name="practice">The practice of fools' practices.</param>
        public void SetPractice(FoolsPractice practice)
        {
            _fool.Practice = practice;
            SetPracticeInfo();
        }

        public void Reset()
        {
            _fool = new FoolNpc();
        }

        public FoolNpc GetNpc()
        {
            return _fool;
        }

        /// <summary>
        /// Set the random practice of fools' practices.
        /// </summary>
        public void SetRandomPractice()
        {
            var practies = Enum.GetValues(typeof(FoolsPractice));
            _fool.Practice = (FoolsPractice)practies.GetValue(new Random().Next(0, practies.Length));
            SetPracticeInfo();
        }

        /// <summary>
        /// Set the bonus and the full practice name according to the practice.
        /// </summary>
        private void SetPracticeInfo()
        {
            _fool.FullPracticeName = PracticesInfo.FoolsPracticeInfo[_fool.Practice].Item1;
            _fool.Bonus = PracticesInfo.FoolsPracticeInfo[_fool.Practice].Item2;
        }
    }
}
