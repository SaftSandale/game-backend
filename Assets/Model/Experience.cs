using System.Collections;


namespace Assets.Model
{
    public class Experience
    {

        public Experience()
        {
            XP = 0;
            Level = 1;
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