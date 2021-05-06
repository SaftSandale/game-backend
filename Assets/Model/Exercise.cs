using LearningGame.Backend.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Model
{
    public class Exercise
    {
        public Exercise(int id, string text, string topic, int difficulty, Dictionary<string, bool> answers)
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
        public Dictionary<string, bool> Answers { get; set; }
        private IEnumerable<string> CorrectAnswers
        {
            get
            {
                if (Answers != null && Answers.Count() != 0)
                {
                    List<string> res = new List<string>();
                    foreach (KeyValuePair<string, bool> ans in Answers)
                    {
                        if (ans.Value == true)
                            res.Add(ans.Key);
                    }
                    return res;
                }
                return null;
            }
        }
        

        public bool CheckAnswers(IEnumerable<string> givenanswers)
        {
            bool equal = givenanswers.OrderBy(x => x).SequenceEqual(CorrectAnswers.OrderBy(x => x));
            return equal;
        }

        public void EditExercise(string newText, string newTopic, Difficulty newDifficulty, Dictionary<string, bool> newAnswers)
        {
            ExerciseText = newText;
            ExerciseTopic = newTopic;
            Difficulty = newDifficulty;
            Answers = newAnswers;

            //edit in Json (edit old or delete and replace with new) (json file bei jedem editieren/löschen/hinzufügen abändern oder bei start einmal umwandeln und json file beim beenden mit neu umgewandelten objekten überschreiben?)
        }
    }
}
