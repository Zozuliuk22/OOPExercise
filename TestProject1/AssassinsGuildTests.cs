using System.Collections.Generic;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using NUnit.Framework;

namespace AnkhMorpork.UnitTests
{
    [TestFixture]
    public class AssassinsGuildTests
    {
        private IEnumerable<Npc> _fakeListNpcs;
        private AssassinNpc _fakeAssassin;
        private AssassinsGuild _guild;
        private Player _player;
        private const string FakeNpcName = "FakeNpc";
        private const string FakePlayerName = "FakePlayerName";
        private const decimal FakeMinReward = 5m;
        private const decimal FakeMaxReward = 15m;

        [SetUp]
        public void SetUp()
        {            
            _fakeAssassin = new AssassinNpc()
            { 
                Name = FakeNpcName,
                MinReward = FakeMinReward,
                MaxReward = FakeMaxReward
            };
            _fakeListNpcs = new List<Npc>() { _fakeAssassin };
            _guild = new AssassinsGuild();
            _guild.CreateNpcs(_fakeListNpcs);
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
        public void PlayGame_ActiveNpcIsNull_PlayerIsAliveFalse()
        {
            _guild.PlayGame(_player);

            Assert.IsFalse(_player.IsAlive);
        }

        [Test]
        public void PlayGame_ActiveNpcIsNotNull_ReturnMessageThatContainsFakeNpcName()
        {
            _guild.CheckContract(10);
            var result = _guild.PlayGame(_player);

            Assert.That(result, Does.Contain(FakeNpcName));
        }
    }
}
