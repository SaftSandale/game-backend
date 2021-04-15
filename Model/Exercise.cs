using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Model
{
    public class Exercise
    {
        public Exercise(int id, string text, int amountcorrectanswers, List<string> answers)
        {
            ID = id;
            ExerciseText = text;
            AmountCorrectAnswers = amountcorrectanswers;
            Answers = answers;
        }


        public int ID { get; set; }
        public string ExerciseText { get; set; }
        public List<string> Answers { get; set; }
        private int AmountCorrectAnswers { get; set; }
        private List<string> CorrectAnswers
        { 
            get
            {
                List<string> res = new List<string>();
                if (Answers != null)
                {
                    return Answers.Take(AmountCorrectAnswers).ToList();
                }
                else return null;
            }
        }


        public bool CheckAnswers(List<string> givenanswers)
        {
            givenanswers.Sort();
            List<string> correctanswers = new List<string>(CorrectAnswers);
            correctanswers.Sort();
            if (givenanswers.SequenceEqual(correctanswers))
                return true;
            else
                return false;
        }
    }
}
