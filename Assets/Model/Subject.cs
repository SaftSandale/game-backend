using Newtonsoft.Json;
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


        public void FillExercises()
        {
            // fill exercise list by subject name##
        }

        public void AddExercises(IEnumerable<Exercise> exercises)
        {
            Exercises.AddRange(exercises);
            //add Exercises To json
        }
        public void RemoveExercises(IEnumerable<int> exerciseids)
        {
            foreach (int id in exerciseids)
                Exercises.RemoveAll(e => e.ID == id);
            //remove Exercises from json
        }


        //public void FillExercises(Difficulty difficulty, int amountOfExercises)
        //{
        //    var exercises = FileHandler.getRandomExercises(this, difficulty, amountOfExercises);
        //    Exercises.Add(difficulty, exercises);
        //}
    }
}
