using PokAEmon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Model
{
    public class Player
    {
        public string PlayerName { get; set; }
        public Experience PlayerExperience { get; set; }

        public Player(string name, Experience xp)
        {
            PlayerName = name;
            PlayerExperience = xp;
        }

    }
}
