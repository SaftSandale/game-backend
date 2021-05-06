using Newtonsoft.Json;
using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using PokAEmon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Controllers
{
    public class ExerciseController
    {
        private static List<Subject> allSubjects;

        public ExerciseController()
        {
            var jsonString = FileHandler.ReadJSON();
            allSubjects = DeserializeJSON(jsonString);
        }

        public List<Subject> DeserializeJSON(string jsonString)
        {
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(jsonString);
            return subjects;
        }

        public static Exercise GetRandomSuitableExercise(Subject subject, string topic, Difficulty difficulty)
        {
            Subject suitableSubject = allSubjects.FirstOrDefault(s => s.SubjectName == subject.SubjectName);
            if (suitableSubject ==  null)
            {
                return null;
            }
            List<Exercise> suitableExercises = suitableSubject.ExercisesByTopicAndDifficulty.FirstOrDefault(e => e.Key == topic).Value.FirstOrDefault(ex => ex.Key == difficulty).Value;

            Random rnd = new Random();
            var randomNumber = rnd.Next(suitableExercises.Count());
            var chosenExercise = suitableExercises[randomNumber];

            return chosenExercise;
        }
    }
}
