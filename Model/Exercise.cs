using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Model
{
    public class Exercise
    {
        public int ID { get; set; }
        public string ExerciseText { get; set; }
        public List<string> Answers { get; set; }
        public List<string> CorrectAnswers { get; set; }

        public bool CheckAnswers(List<string> givenanswers)
        {
            givenanswers.Sort();
            CorrectAnswers.Sort();
            if (givenanswers.SequenceEqual(CorrectAnswers))
                return true;
            else
                return false;
        }
    }
}
