using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    public class ScenarioCreator
    {
        private readonly ThievesGuild _thievesGuild;

        private Meeting _currentMeeting;

        public ScenarioCreator()
        {
            _thievesGuild = new ThievesGuild();
        }

        private string CreateThievesGuildMeeting()
        {
            _thievesGuild.AddTheft();

            if (_thievesGuild.CurrentNumberThefts > _thievesGuild.MaxNumberThefts)
                throw new ArgumentException($"It was already done socially acceptable number of thefts ({_thievesGuild.MaxNumberThefts}) by the Thieves Guild.");
            
            _currentMeeting = new Meeting(_thievesGuild);
            return _currentMeeting.ToString();
        }

        public string CreateRandomGuildMeeting()
        {
            try
            {
                return CreateThievesGuildMeeting();
            }
            catch(ArgumentException)
            {
               return CreateRandomGuildMeeting();
            }
        }

        public void Accept(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                _thievesGuild.PlayGame(player);
        }

        public void Skip(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                _thievesGuild.LoseGame(player);
        }
    }
}
