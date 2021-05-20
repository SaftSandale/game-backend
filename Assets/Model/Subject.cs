using Newtonsoft.Json;
using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Model
{
    public class Subject
    {
        public Subject(string name)
        {
            SubjectName = name;
        }

        [JsonConstructor]
        public Subject(string subjectname, List<Exercise> exercises)
        {
            SubjectName = subjectname;
            Exercises = exercises;
        }


        public string SubjectName { get; set; }
        public List<Exercise> Exercises { get; set; }
        public Dictionary<Difficulty, List<Exercise>> ExercisesByDifficulty
        {
            get
            {
                if (Exercises != null && Exercises.Count() != 0)
                {
                    Dictionary<Difficulty, List<Exercise>> res = new Dictionary<Difficulty, List<Exercise>>();
                    foreach (Exercise e in Exercises)
                    {
                        if (!res.ContainsKey(e.Difficulty))
                            res.Add(e.Difficulty, new List<Exercise>() { e });
                        else
                            res[e.Difficulty].Add(e);
                    }
                    return res;
                }
                else return null;
            }
        }
        public Dictionary<string, Dictionary<Difficulty, List<Exercise>>> ExercisesByTopicAndDifficulty
        {
            get
            {
                if (Exercises != null && Exercises.Count() != 0)
                {
                    Dictionary<string, Dictionary<Difficulty, List<Exercise>>> res = new Dictionary<string, Dictionary<Difficulty, List<Exercise>>>();
                    foreach (Exercise e in Exercises)
                    {
                        if (!res.ContainsKey(e.ExerciseTopic))
                            res.Add(e.ExerciseTopic, new Dictionary<Difficulty, List<Exercise>>( ) { { e.Difficulty, new List<Exercise>() { e } } });
                        else if (!res[e.ExerciseTopic].ContainsKey(e.Difficulty))
                            res[e.ExerciseTopic].Add(e.Difficulty, new List<Exercise>() { e });
                        else
                        {
                            res[e.ExerciseTopic][e.Difficulty].Add(e);
                        }
                    }
                    return res;
                }
                else return null;
            }
        }


        public void CreateExercise(string text, string topic, Difficulty difficulty, List<Answer> answers)
        {
            List<int> allIDs = new List<int>();
            foreach (Subject sub in Cache.AllSubjects)
                foreach(Exercise ex in sub.Exercises)
                    allIDs.Add(ex.ID);

            //int lastID = Cache.AllSubjects.Last().Exercises.Last().ID;
            Exercise newExercise = new Exercise(allIDs.Max() + 1, text, topic, (int)difficulty, answers);
            Exercises.Add(newExercise);
        }
        public void RemoveExercise(Exercise exercise)
        {
            Exercises.Remove(exercise);
        }
    }
}
