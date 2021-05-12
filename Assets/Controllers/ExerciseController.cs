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
        public ExerciseController()
        {

        }

        public static Exercise GetRandomSuitableExercise(Subject subject, string topic, Difficulty difficulty)
        {
            Subject suitableSubject = Cache.AllSubjectsUnusedExercises.FirstOrDefault(s => s.SubjectName == subject.SubjectName);
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

        public static bool CheckIfAnwerIsCorrect(Exercise exercise, string answer)
        {
            var pickedAnwer = exercise.Answers.FirstOrDefault(a => a.Text == answer);
            return pickedAnwer.isCorrect;
        }


    }
}
