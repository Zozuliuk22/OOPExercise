using System.Collections.Generic;
using AnkhMorpork;
using AnkhMorpork.Enums;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class FoolsGuildTests
    {
        private IEnumerable<Npc> _fakeListNpcs;
        private FoolsGuild _guild;
        private Player _player;
        private const string FakePlayerName = "FakePlayerName";
        private const string FakeNpcName = "FakeNpcName";
        private const decimal FakeNpcBonus = 1m;

        [SetUp]
        public void SetUp()
        {
            _fakeListNpcs = new List<Npc>()
            {
                new FoolNpc()
                {
                    Name = FakeNpcName,
                    Practice = FoolsPractice.Gull,
                    FullPracticeName = "Gull",
                    Bonus = FakeNpcBonus
                }
            };

            _guild = new FoolsGuild();
            _guild.CreateNpcs(_fakeListNpcs);
            _guild.GetActiveNpc();
            _player = new Player(FakePlayerName);
        }

        [Test]
        public void LoseGame_InvalidException_ArgumentNullException()
        {
            Assert.That(() => _guild.LoseGame(null), Throws.ArgumentNullException);
        }

        [Test]
        public void PlayGame_InvalidException_ArgumentNullException()
        {
            Assert.That(() => _guild.PlayGame(null), Throws.ArgumentNullException);
        }

        [Test]
        public void PlayGame_WhenCalled_SomeMoneyMoreThanCurrentBudget()
        {
            var currentBudget = _player.CurrentBudget;

            _guild.PlayGame(_player);
            var result = currentBudget + FakeNpcBonus;

            Assert.That(_player.CurrentBudget, Is.EqualTo(result));
        }
    }
}
