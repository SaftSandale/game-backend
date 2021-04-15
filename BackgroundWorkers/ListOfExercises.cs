using LearningGame.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.BackgroundWorkers
{
    public class ListOfExercises
    {
        public ListOfExercises()
        {
            var liste = new List<Exercise>();
            var testExtercise = new Exercise()
            {
                Subject = LearningGame.Controllers.Enums.Subjects.ITS,
                Difficulty = LearningGame.Controllers.Enums.Difficultys.Easy,
                ExerciseText = "Was ist 1 + 1?",
                CorrectAnswer = "2",
                AllAnswers = new List<string>
                {
                    "1",
                    "2",
                    "3"
                }
            };
            liste.Add(testExtercise);
        }
    }
}
