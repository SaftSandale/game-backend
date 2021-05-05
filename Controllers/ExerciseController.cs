using LearningGame.Backend.BackgroundWorkers;
using LearningGame.Backend.Enums;
using LearningGame.Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Controllers
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
            List<Exercise> suitableExercises = (List<Exercise>)suitableSubject.Exercises.Where(e => e.Difficulty == difficulty && e.ExerciseTopic == topic);

            Random rnd = new Random();
            var randomNumber = rnd.Next(suitableExercises.Count());
            var chosenExercise = suitableExercises[randomNumber];

            return chosenExercise;
        }
    }
}
