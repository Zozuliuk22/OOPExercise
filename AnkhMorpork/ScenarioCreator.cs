using System;
using AnkhMorpork.Guilds;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

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

        public void InitialiseBeggarsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _beggarsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
            else
                throw new ArgumentException("The path to the file is not correct.");
        }        

        public void InitialiseFoolsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _foolsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
            else
                throw new ArgumentException("The path to the file is not correct.");
        }        

        public void InitialiseAssassinsGuild(string path)
        {
            if (path.EndsWith(".json"))
            {
                _assassinsGuild.CreateNpcs(DataLoader.LoadNpcsFromJson(path));
            }
            else
                throw new ArgumentException("The path to the file is not correct.");
        }       

        public Meeting CreateRandomGuildMeeting()
        {
            return (Meeting)_methodsCreateGuild[new Random().Next(0, _methodsCreateGuild.Count)].Invoke(this, null);  
        }

        private Meeting CreateThievesGuildMeeting()
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
                return _currentMeeting;
            }
        }

        private Meeting CreateBeggarsGuildMeeting()
        {
            _currentMeeting = new Meeting(_beggarsGuild, _beggarsGuild.GetNpc());
            return _currentMeeting;
        }

        private Meeting CreateAssassinsGuildMeeting()
        {
            _currentMeeting = new Meeting(_assassinsGuild);
            return _currentMeeting;
        }

        private Meeting CreateFoolsGuildMeeting()
        {
            _currentMeeting = new Meeting(_foolsGuild, _foolsGuild.GetNpc());
            return _currentMeeting;
        }

        public string Accept(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                return _thievesGuild.PlayGame(player);

            if(_currentMeeting.Guild is BeggarsGuild)
                return _beggarsGuild.PlayGame(player);

            if(_currentMeeting.Guild is FoolsGuild)
                return _foolsGuild.PlayGame(player);

            if (_currentMeeting.Guild is AssassinsGuild)
                return _assassinsGuild.PlayGame(player);

            return "This is unknown guild.";
        }

        public string Skip(Player player)
        {
            if (_currentMeeting.Guild is ThievesGuild)
                return _thievesGuild.LoseGame(player);

            if (_currentMeeting.Guild is BeggarsGuild)
                return _beggarsGuild.LoseGame(player);

            if (_currentMeeting.Guild is FoolsGuild)
                return _foolsGuild.LoseGame(player);

            if (_currentMeeting.Guild is AssassinsGuild)
                return _assassinsGuild.LoseGame(player);

            return "This is unknown guild.";
        }

        public void UseEnteredFee(decimal fee)
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
