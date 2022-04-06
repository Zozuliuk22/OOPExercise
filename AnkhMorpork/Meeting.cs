using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnkhMorpork
{
    internal class Meeting
    {
        private Guild _guild;

        internal Guild Guild { get { return _guild; } }

        internal Meeting(Guild guild)
        {
            _guild = guild;            
        }       

        public override string ToString()
        {
            return $"You met the {Guild}.";
        }
    }
}
