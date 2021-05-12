using PokAEmon.Enums;
using System.Collections;


namespace Assets.Model
{
    public class Experience
    {

        public Experience()
        {
            XP = 0;
            Level = 1;
            Bonus = 1;
        }
        public float XP { get; private set; }
        public int Level { get; private set; }
        public float Bonus { get; private set; }

        public void setBonus(float bonus)
        {
            Bonus += bonus;
        }

        public void resetBonus()
        {
            Bonus = 1;
        }

        public void addXP(Difficulty dif)
        {
            this.XP += ((int)dif * Bonus );

            int neededXP = Level * 150;

            if(this.XP > neededXP)
            {
                Level++;
            }
        }

        
    }
}