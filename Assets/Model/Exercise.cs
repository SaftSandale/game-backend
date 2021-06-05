using Newtonsoft.Json;
using PokAEmon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokAEmon.Model
{
    /// <summary>
    /// Model für alle Aufgaben.
    /// </summary>
    public class Exercise
    {
        #region Properties

        /// <summary>
        /// ID, um eine Aufgabe unverwechselbar zu unterscheiden.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Text der Aufgabe.
        /// </summary>
        public string ExerciseText { get; set; }

        /// <summary>
        /// Thema der Aufgabe.
        /// </summary>
        public string ExerciseTopic { get; set; }

        /// <summary>
        /// Schwierigkeit der Aufgabe.
        /// </summary>
        public Difficulty Difficulty { get; set; }

        /// <summary>
        /// Liste von Antworten der Aufgabe.
        /// </summary>
        public List<Answer> Answers { get; set; }

        /// <summary>
        /// Liste aller richtigen Antworten.
        /// </summary>
        private IEnumerable<Answer> CorrectAnswers
        {
            get
            {
                if (Answers != null && Answers.Count() != 0)
                {
                    IEnumerable<Answer> res = Answers.Where(a => a.IsCorrect == true);
                    return res;
                }
                return null;
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor um neue Aufgabe anzulegen, wenn der Nutzer eine Aufgabe hinzufügt.
        /// </summary>
        public Exercise()
        {
            ExerciseText = "";
            ExerciseTopic = "";
            Difficulty = Difficulty.Easy;
            Answers = new List<Answer>();
        }

        /// <summary>
        /// JSON Konstruktor, der genutzt wird, um JSON String in Exercise Model zu parsen.
        /// </summary>
        /// <param name="id">ID der Aufgabe.</param>
        /// <param name="text">Text der Aufgabe.</param>
        /// <param name="topic">Thema der Aufgabe.</param>
        /// <param name="difficulty">Schwierigkeit der Aufgabe.</param>
        /// <param name="answers">Liste aller Antworten der Aufgabe.</param>
        [JsonConstructor]
        public Exercise(int id, string text, string topic, int difficulty, List<Answer> answers)
        {
            ID = id;
            ExerciseText = text;
            ExerciseTopic = topic;
            Difficulty = (Difficulty)difficulty;
            Answers = answers;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Mischt alle Anwtorten in eine zufällige Reihenfolge.
        /// </summary>
        /// <returns>Liste der zufällig gemischten Antworten.</returns>
        public List<Answer> GetShuffledAnswers()
        {
            var rnd = new Random();
            var randomizedAnswers = new List<Answer>(Answers.OrderBy(item => rnd.Next()));
            return randomizedAnswers;
        }

        /// <summary>
        /// Bearbeitet eine Aufgabe mit übergebenen Werten.
        /// </summary>
        /// <param name="newText">Neuer Text der Aufgabe.</param>
        /// <param name="newTopic">Neues Thema der Aufgabe.</param>
        /// <param name="newDifficulty">Neue Schwierigkeit der Aufgabe.</param>
        /// <param name="newAnswers">Neue Liste an möglichen Antworten der Aufgabe.</param>
        public void EditExercise(string newText, string newTopic, Difficulty newDifficulty, List<Answer> newAnswers)
        {
            ExerciseText = newText;
            ExerciseTopic = newTopic;
            Difficulty = newDifficulty;
            Answers = newAnswers;
        }
        #endregion
    }
}
