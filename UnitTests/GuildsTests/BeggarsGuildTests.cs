using System.Collections.Generic;
using AnkhMorpork;
using AnkhMorpork.Enums;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BeggarsGuildTests
    {
        private IEnumerable<Npc> _fakeListNpcs;
        private BeggarsGuild _guild;
        private Player _player;
        private const string FakePlayerName = "FakePlayerName";
        private const string FakeNpcName = "FakeNpcName";

        [SetUp]
        public void SetUp()
        {
            _fakeListNpcs = new List<Npc>()
            {
                new BeggarNpc()
                {
                    Name = FakeNpcName,
                    Practice = BeggarsPractice.Twitchers,
                    FullPracticeName = "Twitchers",
                    Fee = 3m
                }                
            };

            _guild = new BeggarsGuild();
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
        public void LoseGame_WhenCalled_PlayerIsAliveFalse()
        {
            _guild.LoseGame(_player);

            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_InvalidException_ArgumentNullException()
        {
            Assert.That(() => _guild.PlayGame(null), Throws.ArgumentNullException);
        }        

        [Test]
        public void PlayGame_CurrentBudgetIsLessThanFee_PlayerIsAliveFalse()
        {
            _player.LoseMoney(99);
            _guild.PlayGame(_player);

            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_CurrentBudgetIsNotLessThanFee_PlayerIsAliveTrue()
        {
            _guild.PlayGame(_player);

            Assert.IsTrue(_player.IsAlive);
        }

        [Test]
        public void PlayGame_ActiveNpcIsBeerNeeder_PlayerIsAliveFalse()
        {
            _fakeListNpcs = new List<Npc>()
            {
                new BeggarNpc
                {
                    Name = FakeNpcName,
                    Practice = BeggarsPractice.BeerNeeders,
                    FullPracticeName = "People With Placards Saying \"Why lie ? I need a beer.\"",
                    Fee = 0
                }
            };
            _guild = new BeggarsGuild();
            _guild.CreateNpcs(_fakeListNpcs);
            _guild.GetActiveNpc();
            _guild.PlayGame(_player);

            Assert.IsFalse(_player.IsAlive);
        }
    }
}
