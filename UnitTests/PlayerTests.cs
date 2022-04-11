using NUnit.Framework;
using AnkhMorpork;

namespace UnitTests
{
    public class Tests
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
            _player = new Player("TestName");
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Player_InvalidError_ThrowArgumentNullException(string name)
        {
            Assert.That(() => new Player(name), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase(-10)]
        public void EarnMoney_InvalidError_ThrowArgumentException(decimal bonus)
        {
            Assert.That(() => _player.EarnMoney(bonus), Throws.ArgumentException);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void EarnMoney_EarnSomeMoney_SomeMoneyMoreThanCurrentBudget(decimal bonus)
        {
            var currentBudget = _player.CurrentBudget;

            _player.EarnMoney(bonus);
            var result = currentBudget + bonus;

            Assert.That(_player.CurrentBudget, Is.EqualTo(result));
        }

        [Test]
        [TestCase(2)]
        public void EarnMoney_WhenCalled_ScoreOneMoreThanInitial(decimal bonus)
        {
            var initialScore = _player.Score;

            _player.EarnMoney(bonus);
            var result = initialScore + 1;

            Assert.That(_player.Score, Is.EqualTo(result));
        }

        [Test]
        [TestCase(-10)]
        [TestCase(150)]
        public void LoseMoney_InvalidError_ThrowArgumentException(decimal fee)
        {
            Assert.That(() => _player.LoseMoney(fee), Throws.ArgumentException);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void LoseMoney_LoseSomeMoney_SomeMoneyLessThanCurrentBudget(decimal fee)
        {
            var currentBudget = _player.CurrentBudget;

            _player.LoseMoney(fee);
            var result = currentBudget - fee;

            Assert.That(_player.CurrentBudget, Is.EqualTo(result));
        }

        [Test]
        [TestCase(2)]
        public void LoseMoney_WhenCalled_ScoreOneMoreThanInitial(decimal fee)
        {
            var initialScore = _player.Score;

            _player.LoseMoney(fee);
            var result = initialScore + 1;

            Assert.That(_player.Score, Is.EqualTo(result));
        }

        [Test]
        public void ToDie_WhenCalled_SetIsAliveFalse()
        {
            _player.ToDie();

            Assert.That(_player.IsAlive, Is.False);
        }
    }
}