using System.Collections.Generic;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using NUnit.Framework;

namespace AnkhMorpork.UnitTests
{
    [TestFixture]
    public class ThievesGuildTests
    {
        private ThievesGuild _guild;
        private Player _player;
        private const string FakePlayerName = "FakePlayerName";

        [SetUp]
        public void SetUp()
        {
            _guild = new ThievesGuild();
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
        public void PlayGame_CurrentBudgetIsLessThanDefaultFee_PlayerIsAliveFalse()
        {
            _player.LoseMoney(99);
            _guild.PlayGame(_player);

            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_CurrentBudgetIsNotLessThanDefaultFee_ReturnMessageThatContainsUnknownName()
        {
            var result = _guild.PlayGame(_player);

            Assert.That(result, Does.Contain("Unknown").IgnoreCase);
        }
    }
}
