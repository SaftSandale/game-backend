using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Model
{
    public class Exercise
    {
        public Exercise(int id, string text, string topic, int difficulty, List<Answer> answers)
        {
            ID = id;
            ExerciseText = text;
            ExerciseTopic = topic;
            Difficulty = (Difficulty)difficulty;
            Answers = answers;
        }


        public int ID { get; set; }
        public string ExerciseText { get; set; }
        public string ExerciseTopic { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<Answer> Answers { get; set; }
        private IEnumerable<Answer> CorrectAnswers
        {
            get
            {
                if (Answers != null && Answers.Count() != 0)
                {
                    IEnumerable<Answer> res = Answers.Where(a => a.isCorrect == true);
                    return res;
                }
                return null;
            }
        }
        
        public List<Answer> GetShuffledAnswers()
        {
            Answers.Shuffle();
            return Answers;
        }

        public bool CheckAnswers(IEnumerable<Answer> givenanswers)
        {
            bool equal = givenanswers.OrderBy(x => x.Text).SequenceEqual(CorrectAnswers.OrderBy(x => x.Text));
            return equal;
        }

        public void EditExercise(string newText, string newTopic, Difficulty newDifficulty, List<Answer> newAnswers)
        {
            ExerciseText = newText;
            ExerciseTopic = newTopic;
            Difficulty = newDifficulty;
            Answers = newAnswers;
        }
    }
}
