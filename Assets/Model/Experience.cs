using Newtonsoft.Json;
using System.Collections;


namespace PokAEmon.Model
{
    public class Experience
    {

        public Experience()
        {
            XP = 0;
            Level = 1;
        }

        [JsonConstructor]
        public Experience(int xp, int lvl)
        {
            XP = xp;
            Level = lvl;
        }

        public int XP { get; private set; }
        public int Level { get; private set; }

        public void addXP(int xp)
        {
            this.XP += xp;

            int neededXP = Level * 150;

            if(this.XP > neededXP)
            {
                Level++;
            }
        }

        
    }
}