using System;
using AnkhMorpork.Guilds;
using AnkhMorpork.NPCs;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    public class ScenarioCreator
    {
        private readonly ThievesGuild _thievesGuild;
        private readonly BeggarsGuild _beggarsGuild;
        private readonly FoolsGuild _foolsGuild;
        private readonly AssassinsGuild _assassinsGuild;

        private Meeting _currentMeeting;

        private List<MethodInfo> _methodsCreateGuild;

        public ScenarioCreator()
        {
            _thievesGuild = new ThievesGuild();
            _beggarsGuild = new BeggarsGuild();
            _foolsGuild = new FoolsGuild();
            _assassinsGuild = new AssassinsGuild();

            _methodsCreateGuild = typeof(ScenarioCreator)
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name.StartsWith("Create"))
                .ToList();
        }

        private string CreateThievesGuildMeeting()
        {
            _thievesGuild.AddTheft();

            if (_thievesGuild.CurrentNumberThefts > _thievesGuild.MaxNumberThefts)
            {
                var method = _methodsCreateGuild.First(m => m.Name.Contains("Thieves"));
                _methodsCreateGuild.Remove(method);
                return CreateRandomGuildMeeting();
            }
            else
            {
                _currentMeeting = new Meeting(_thievesGuild);
                return _currentMeeting.ToString();
            }           
        }

        public void InitialiseBeggarsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _beggarsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
        }

        private string CreateBeggarsGuildMeeting()
        {
            try
            {
                _currentMeeting = new Meeting(_beggarsGuild, _beggarsGuild.GetNpc());
                return _currentMeeting.ToString();
            }
            catch (Exception ex)
            {
                _currentMeeting = null;
                return ex.Message;
            }
        }

        public void InitialiseFoolsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _foolsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
        }

        private string CreateFoolsGuildMeeting()
        {
            try
            {
                _currentMeeting = new Meeting(_foolsGuild, _foolsGuild.GetNpc());
                return _currentMeeting.ToString();
            }
            catch (Exception ex)
            {
                _currentMeeting = null;
                return ex.Message;
            }
        }

        public void InitialiseAssassinsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _assassinsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
        }

        private string CreateAssassinsGuildMeeting()
        {
            try
            {
                _currentMeeting = new Meeting(_assassinsGuild);
                return _currentMeeting.ToString();
            }
            catch (Exception ex)
            {
                _currentMeeting = null;
                return ex.Message;
            }
        }

        public string CreateRandomGuildMeeting()
        {
            var meeting = _methodsCreateGuild[new Random().Next(0, _methodsCreateGuild.Count)].Invoke(this, null).ToString();
            if(_currentMeeting.Npc is not null)
                meeting += "\n" + _currentMeeting.Npc.ToString();
            return meeting;            
        }

        public void Accept(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                _thievesGuild.PlayGame(player);
            if(_currentMeeting.Guild is BeggarsGuild)
                _beggarsGuild.PlayGame(player);
            if(_currentMeeting.Guild is FoolsGuild)
                _foolsGuild.PlayGame(player);
            if (_currentMeeting.Guild is AssassinsGuild)
                _assassinsGuild.PlayGame(player);
        }

        public void Skip(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                _thievesGuild.LoseGame(player);
            if (_currentMeeting.Guild is BeggarsGuild)
                _beggarsGuild.LoseGame(player);
            if (_currentMeeting.Guild is FoolsGuild)
                _foolsGuild.LoseGame(player);
            if (_currentMeeting.Guild is AssassinsGuild)
                _assassinsGuild.LoseGame(player);
        }

        public void SetEnteredFee(decimal fee)
        {
            var guild = _currentMeeting.Guild as AssassinsGuild;
            if(guild is not null)
            {
                if (guild.CheckContract(fee))
                    _currentMeeting.Guild.GetNpc();
            }
        }
    }
}
