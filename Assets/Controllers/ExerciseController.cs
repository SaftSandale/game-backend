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
    /// <summary>
    /// ExerciseController führt Aufgabenbezogene Operationen durch.
    /// </summary>
    public class ExerciseController
    {
        #region Methods

        /// <summary>
        /// Ermittelt aus dem Chache eine zufällige Aufgabe, die das passende Fach, die passende Schwierigkeit und das passende Thema hat.
        /// </summary>
        /// <param name="subject">Das Fach, dem die Aufgabe angehören soll.</param>
        /// <param name="topic">Das Thema, dem die Aufgabe angehören soll.</param>
        /// <param name="difficulty">Die Schwierigkeit, die Aufgabe haben soll.</param>
        /// <returns>Gibt eine Aufgabe zurück.</returns>
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

        /// <summary>
        /// Überprüft, ob die angegebene Antwort richtig ist.
        /// </summary>
        /// <param name="exercise">Die Aufgabe, die beantwortet wurde.</param>
        /// <param name="answer">String der Antwort, die ausgewählt wurde.</param>
        /// <returns>Gibt true oder false zurück.</returns>
        public static bool CheckIfAnwerIsCorrect(Exercise exercise, string answer)
        {
            var pickedAnswer = exercise.Answers.FirstOrDefault(a => a.Text == answer);
            return pickedAnswer.IsCorrect;
        }
        #endregion
    }
}
