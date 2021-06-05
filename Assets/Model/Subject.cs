using Newtonsoft.Json;
using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using System.Collections.Generic;
using System.Linq;

namespace PokAEmon.Model
{
    /// <summary>
    /// Model für alle Fächer. 
    /// </summary>
    public class Subject
    {
        #region Properties

        /// <summary>
        /// Speichert den Namen des Fachs.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Speichert eine Liste der Aufgaben des Fachs.
        /// </summary>
        public List<Exercise> Exercises { get; set; }

        /// <summary>
        /// Speichert alle Aufgaben nach Schwierigkeit sortiert.
        /// </summary>
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

        /// <summary>
        /// Speichert alle Aufgaben nach Schwierigkeit, Fach und Thema sortiert.
        /// </summary>
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
                            res.Add(e.ExerciseTopic, new Dictionary<Difficulty, List<Exercise>>() { { e.Difficulty, new List<Exercise>() { e } } });
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
        #endregion

        #region Contructor

        /// <summary>
        /// Erstellt ein neues Fach mit übergebenem Namen.
        /// </summary>
        /// <param name="name">Name, den das Fach haben soll.</param>
        public Subject(string name)
        {
            SubjectName = name;
        }

        /// <summary>
        /// JSON Konstruktor, der aufgerufen wird, um den String aus der JSON Datei in diese Klasse zu Parsen.
        /// </summary>
        /// <param name="subjectname">Name des Fachs.</param>
        /// <param name="exercises">Liste aller Aufgaben des Fachs.</param>
        [JsonConstructor]
        public Subject(string subjectname, List<Exercise> exercises)
        {
            SubjectName = subjectname;
            Exercises = exercises;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Erstellt eine Aufgabe mit übergebenem Text, Thema, Schwierigkeit und einer Liste an Antworten.
        /// </summary>
        /// <param name="text">Text der Aufgabe.</param>
        /// <param name="topic">Thema der Aufgabe.</param>
        /// <param name="difficulty">Schwierigkeit der Aufgabe.</param>
        /// <param name="answers">Liste aller Antworten der Aufgabe.</param>
        public void CreateExercise(string text, string topic, Difficulty difficulty, List<Answer> answers)
        {
            List<int> allIDs = new List<int>();
            foreach (Subject sub in DataCache.AllSubjects)
                if (sub.Exercises != null)
                {
                    foreach (Exercise ex in sub.Exercises)
                    {
                        allIDs.Add(ex.ID);
                    }
                }

            //int lastID = Cache.AllSubjects.Last().Exercises.Last().ID;
            if (Exercises == null)
            {
                Exercises = new List<Exercise>();
            }
            Exercise newExercise = new Exercise(allIDs.Max() + 1, text, topic, (int)difficulty, answers);
            Exercises.Add(newExercise);
        }

        /// <summary>
        /// Löscht eine ausgewählte Aufgabe.
        /// </summary>
        /// <param name="exercise">Die zu löschende Aufgabe.</param>
        public void RemoveExercise(Exercise exercise)
        {
            Exercises.Remove(exercise);
        }
        #endregion
    }
}
