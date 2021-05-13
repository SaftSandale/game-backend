using PokAEmon.Enums;
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
        internal class QuestionCache
        {
            public int Index { get; private set; }

            private int BonusLimit { get; set; }

            public QuestionCache(int limit)
            {
                Index = 0;
                BonusLimit = limit;
            }

            public void AddElement(bool question)
            {
                if (question == true)
                {
                    Index++;
                }
                else
                {
                    Index = 0;
                }
            }

            public int CalcBonus()
            {
                return (Index % BonusLimit);
            }
        }

        public string PlayerName { get; set; }
        public Experience PlayerExperience { get; set; }
        private QuestionCache QC { get; set; }

        public Player(string name, Experience xp)
        {
            PlayerName = name;
            PlayerExperience = xp;
            QC = new QuestionCache(10);
        }

        public void AddXP(Difficulty dif, bool answerStatus)
        {
            QC.AddElement(answerStatus);
            if(QC.CalcBonus() == 0)
            {
                PlayerExperience.resetBonus();
            }
            else
            {
                PlayerExperience.setBonus((float)(0.05 * QC.CalcBonus()));
            }
            PlayerExperience.addXP(dif);
            
        }



    }
}
