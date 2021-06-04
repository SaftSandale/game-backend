using Newtonsoft.Json;
using PokAEmon.Enums;
using PokAEmon.Model;
using System.Collections.Generic;
using System.Linq;

namespace PokAEmon.BackgroundWorkers
{
    /// <summary>
    /// Cache ist die Klasse, die alle wichtigen Daten speichert, die während des Spiels benötigt werden.
    /// </summary>
    public class Cache
    {
        #region Properties

        /// <summary>
        /// Anzahl der Aufgaben, die der Cache speichern soll.
        /// </summary>
        private int MaxElements { get; set; }

        /// <summary>
        /// Queue, der die IDs der letzten Aufgaben speichert. Die Anzahl wird mit maxElements festgelegt.
        /// </summary>
        private static Queue<int> QuestionIdCache { get; set; }

        /// <summary>
        /// Speichert Daten des aktuellen Spielers. 
        /// </summary>
        public static Player CurrentPlayer { get; set; }

        /// <summary>
        /// Speichert eine Liste aller Standard Nachrichten, die im Spiel ausgegeben werden können.
        /// </summary>
        public static List<TextLine> AllTextLines { get; set; }

        /// <summary>
        /// Speichert eine Liste aller besonderen Nachrichten, die im Spiel ausgegeben werden können.
        /// </summary>
        public static List<TextLine> AllSpecialTextLines { get; set; }

        /// <summary>
        /// Speichert eine Liste alle Fächer mit Aufgaben, die im Spiel gestellt werden können.
        /// </summary>
        public static List<Subject> AllSubjects { get; set; }

        /// <summary>
        /// Speichert eine Liste aller Facher mit Aufgaben, die im aktuellen Speildurchlauf noch nicht gestellt wurden.
        /// </summary>
        public static List<Subject> AllSubjectsUnusedExercises
        { 
            get
            {
                if (AllSubjects != null)
                {
                    List<Subject> res = new List<Subject>();
                    foreach (Subject sub in AllSubjects)
                    {
                        res.Add(new Subject(sub.SubjectName));
                        foreach(Exercise ex in sub.Exercises)
                        {
                            if (!QuestionIdCache.Contains(ex.ID))
                            {
                                if (res.FirstOrDefault(s => s.SubjectName == sub.SubjectName).Exercises != null)
                                {
                                    res.FirstOrDefault(s => s.SubjectName == sub.SubjectName).Exercises.Add(ex);
                                }
                                else
                                {
                                    res.FirstOrDefault(s => s.SubjectName == sub.SubjectName).Exercises = new List<Exercise>();
                                    res.FirstOrDefault(s => s.SubjectName == sub.SubjectName).Exercises.Add(ex);
                                }
                            }
                        }
                    }
                    return res;
                }
                return null;
            } 
        }

        /// <summary>
        /// Speichert die Anzahl der richtig beantworteten Aufgaben der Schwierigkeit leicht.
        /// </summary>
        public static Dictionary<string, int> AmountCorrectEasyExercises { get; set; }

        /// <summary>
        /// Speichert die Anzahl der richtig beantworteten Aufgaben der Schwierigkeit mittelschwer.
        /// </summary>
        public static Dictionary<string, int> AmountCorrectMediumExercises { get; set; }

        /// <summary>
        /// Speichert die Anzahl der richtig beantworteten Aufgaben der Schwierigkeit schwer.
        /// </summary>
        public static Dictionary<string, int> AmountCorrectHardExercises { get; set; }

        /// <summary>
        /// Speichert die Anzahl der insgesamt beantworteten Aufgaben.
        /// </summary>
        public static int TotalAnsweredQuestions { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor, der die Anzahl der zu Speichernden Elemente setzt und weitere Properties befüllt.
        /// </summary>
        /// <param name="anzElements">Maximale Anzahl, die der Cache speichern soll.</param>
        public Cache(int anzElements)
        {
            MaxElements = anzElements;
            QuestionIdCache = new Queue<int>();
            GetAllSubjects();
            GetAllTextLines();
            GetAllSpecialTextLines();
            FillDictionarys();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deserialisiert JSON String mit allen Fächern und Aufgaben in das Subject Model und befüllt die Property AllSubjects mit diesen Daten.
        /// </summary>
        private void GetAllSubjects()
        {
            string jsonString = FileHandler.ReadExerciseJSON();
            List<Subject> allSubjectsFromJson = JsonConvert.DeserializeObject<List<Subject>>(jsonString);
            if (allSubjectsFromJson != null)
            {
                AllSubjects = allSubjectsFromJson;
            }
            else
            {
                AllSubjects = new List<Subject>();
            }
        }

        /// <summary>
        /// Deserialisiert JSON String mit allen standard Nachrichten, die im Spiel ausgegeben werden können in das TextLine Model und befüllt die Property AllTextLines mit diesen Daten.
        /// </summary>
        private void GetAllTextLines()
        {
            string jsonString = FileHandler.ReadTextLineJSON();
            List<TextLine> allTextLinesFromJson = JsonConvert.DeserializeObject<List<TextLine>>(jsonString);
            if (allTextLinesFromJson != null)
            {
                AllTextLines = allTextLinesFromJson;
            }
            else
            {
                AllTextLines = new List<TextLine>();
            }
        }

        /// <summary>
        /// Deserialisiert JSON String mit allen speziellen Nachrichten, die im Spiel ausgegeben werden können in das TextLine Model und befüllt die Property AllTextLines mit diesen Daten.
        /// </summary>
        private void GetAllSpecialTextLines()
        {
            string jsonString = FileHandler.ReadSpecialTextLineJSON();
            List<TextLine> allSpecialTextLinesFromJson = JsonConvert.DeserializeObject<List<TextLine>>(jsonString);
            if (allSpecialTextLinesFromJson != null)
            {
                AllSpecialTextLines = allSpecialTextLinesFromJson;
            }
            else
            {
                AllSpecialTextLines = new List<TextLine>();
            }
        }

        /// <summary>
        /// Fügt der Property QuestionIdCache eine ID einer Aufgabe hinzu. Sollte maxElements überschritten werden, wird der letzte Eintrag aus dem QuestionIdCache entfernt.
        /// </summary>
        /// <param name="ID"></param>
        public void AddElement(int ID)
        {
            if(QuestionIdCache.Count >= MaxElements)
            {
                QuestionIdCache.Dequeue();
            }
            QuestionIdCache.Enqueue(ID);
        }

        /// <summary>
        /// Speichert den aktuellen Pool an Aufgaben in die JSON Datei, sodass Änderungenn an Aufgaben beim nächsten Spielstart verfügbar sind.
        /// </summary>
        public static void SaveCacheToJson()
        {
            string subjectJsonString = JsonConvert.SerializeObject(AllSubjects);
            FileHandler.WriteExerciseJson(subjectJsonString);
        }

        /// <summary>
        /// Befüllt die Dictionarys, die die Anzahl der richtigen beantworteten Aufgaben speichern.
        /// </summary>
        private void FillDictionarys()
        {
            AmountCorrectEasyExercises = new Dictionary<string, int>();
            AmountCorrectMediumExercises = new Dictionary<string, int>();
            AmountCorrectHardExercises = new Dictionary<string, int>();
            AmountCorrectEasyExercises.Add("DB", 0);
            AmountCorrectEasyExercises.Add("OOP", 0);
            AmountCorrectEasyExercises.Add("VC", 0);
            AmountCorrectEasyExercises.Add("PP", 0);
            AmountCorrectEasyExercises.Add("Testing", 0);
            AmountCorrectMediumExercises.Add("DB", 0);
            AmountCorrectMediumExercises.Add("OOP", 0);
            AmountCorrectMediumExercises.Add("VC", 0);
            AmountCorrectMediumExercises.Add("PP", 0);
            AmountCorrectMediumExercises.Add("Testing", 0);
            AmountCorrectHardExercises.Add("DB", 0);
            AmountCorrectHardExercises.Add("OOP", 0);
            AmountCorrectHardExercises.Add("VC", 0);
            AmountCorrectHardExercises.Add("PP", 0);
            AmountCorrectHardExercises.Add("Testing", 0);
        }

        /// <summary>
        /// Updated die Anzahl der richtig beantworteten Aufgaben, wenn eine Aufgabe richtig beantwortet wurde und zählt TotalAnsweredQuestions hoch.
        /// </summary>
        /// <param name="exercise">Die Aufgabe, die beantwortet wurde.</param>
        /// <param name="isCorrect">Boolean, ob die Aufgabe richtig beantwortet wurde.</param>
        public static void SaveAmountCorrectAnsweredQuestion(Exercise exercise, bool isCorrect)
        {
            if (isCorrect)
            {
                switch (exercise.Difficulty)
                {
                    case Difficulty.Easy:
                        AmountCorrectEasyExercises[exercise.ExerciseTopic]++;
                        break;
                    case Difficulty.Medium:
                        AmountCorrectMediumExercises[exercise.ExerciseTopic]++;
                        break;
                    case Difficulty.Hard:
                        AmountCorrectHardExercises[exercise.ExerciseTopic]++;
                        break;
                }
            }
            TotalAnsweredQuestions++;
        }

        /// <summary>
        /// Gibt die Anzahl der richtig Beantworteten Aufgaben für eine Schwierigkeit zurück.
        /// </summary>
        /// <param name="difficulty">Die gewünschte Schwierigkeit.</param>
        /// <returns></returns>
        public static int GetAmountOfCorrectAnsweredExercisesForDifficulty(Difficulty difficulty)
        {
            int amountCorrectlyAnsweredExercises = 0;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    foreach (var topic in AmountCorrectEasyExercises)
                    {
                        amountCorrectlyAnsweredExercises += topic.Value;
                    }
                    break;
                case Difficulty.Medium:
                    foreach (var topic in AmountCorrectMediumExercises)
                    {
                        amountCorrectlyAnsweredExercises += topic.Value;
                    }
                    break;
                case Difficulty.Hard:
                    foreach (var topic in AmountCorrectHardExercises)
                    {
                        amountCorrectlyAnsweredExercises += topic.Value;
                    }
                    break;
            }
            return amountCorrectlyAnsweredExercises;
        }
        #endregion
    }
}