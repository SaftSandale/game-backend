using Newtonsoft.Json;
using PokAEmon.Enums;
using PokAEmon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.BackgroundWorkers
{
    public class Cache
    {
        #region Properties
        /// <summary>
        /// Anzahl der Fragen, die der Cache speichern soll.
        /// </summary>
        private int maxElements { get; set; }
        /// <summary>
        /// Queue, der die IDs der letzten Fragen speichert. Die Anzahl wird mit maxElements festgelegt.
        /// </summary>
        private static Queue<int> QuestionIdCache { get; set; }
        //private List<Player> AllPlayers { get; set; }
        /// <summary>
        /// Speichert Daten des aktuellen Spielers. 
        /// </summary>
        public static Player CurrentPlayer { get; set; }
        /// <summary>
        /// Speichert eine Liste aller Nachrichten, die im Spiel ausgegeben werden k�nnen.
        /// </summary>
        public static List<TextLine> AllTextLines { get; set; }
        /// <summary>
        /// Speichert eine Liste aller besonderen Nachrichten, die im Spiel ausgegeben werden k�nnen.
        /// </summary>
        public static List<TextLine> AllSpecialTextLines { get; set; }
        /// <summary>
        /// Speichert eine Liste alle F�cher mit Fragen, die im Spiel gestellt werden k�nnen.
        /// </summary>
        public static List<Subject> AllSubjects { get; set; }
        /// <summary>
        /// Speichert eine Liste aller Facher mit Fragen, die im aktuellen Speildurchlauf noch nicht gestellt wurden.
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

        public static Dictionary<string, int> AmountCorrectEasyExercises { get; set; }
        public static Dictionary<string, int> AmountCorrectMediumExercises { get; set; }
        public static Dictionary<string, int> AmountCorrectHardExercises { get; set; }
        public static int TotalAnsweredQuestions { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Konstruktor, der die Anzahl der zu Speichernden Elemente setzt und weitere Properties bef�llt.
        /// </summary>
        /// <param name="anzElements">Maximale Anzahl, die der Cache speichern soll.</param>
        public Cache(int anzElements)
        {
            maxElements = anzElements;
            QuestionIdCache = new Queue<int>();
            GetAllSubjects();
            GetAllTextLines();
            GetAllSpecialTextLines();
            FillDictionarys();
            //GetAllPlayers();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Deserialisiert JSON String mit allen F�chern und Aufgaben in das Subject Model und bef�llt die Property AllSubjects mit diesen Daten.
        /// </summary>
        private void GetAllSubjects()
        {
            List<Subject> res = JsonConvert.DeserializeObject<List<Subject>>(FileHandler.ReadExerciseJSON());
            if (res != null)
            {
                AllSubjects = res;
            }
            else
            {
                AllSubjects = new List<Subject>();
            }
        }

        //private void GetAllPlayers()
        //{
        //    List<Player> res = JsonConvert.DeserializeObject<List<Player>>(FileHandler.ReadPlayersJSON());
        //    if (res != null) AllPlayers = res;
        //    else AllPlayers = new List<Player>();
        //}

        /// <summary>
        /// Deserialisiert JSON String mit allen Nachrichten, die im Spiel ausgegeben werden k�nnen in das TextLine Model und bef�llt die Property AllTextLines mit diesen Daten.
        /// </summary>
        private void GetAllTextLines()
        {
            List<TextLine> res = JsonConvert.DeserializeObject<List<TextLine>>(FileHandler.ReadTextLineJSON());
            if (res != null)
            {
                AllTextLines = res;
            }
            else
            {
                AllTextLines = new List<TextLine>();
            }
        }

        private void GetAllSpecialTextLines()
        {
            List<TextLine> res = JsonConvert.DeserializeObject<List<TextLine>>(FileHandler.ReadSpecialTextLineJSON());
            if (res != null)
            {
                AllSpecialTextLines = res;
            }
            else
            {
                AllSpecialTextLines = new List<TextLine>();
            }
        }

        /// <summary>
        /// F�gt der Property QuestionIdCache eine ID einer Frage hinzu. Sollte maxElements �berschritten werden, wird der letzte Eintrag aus dem QuestionIdCache entfernt.
        /// </summary>
        /// <param name="ID"></param>
        public void addElement(int ID)
        {
            if(QuestionIdCache.Count >= maxElements)
            {
                QuestionIdCache.Dequeue();
            }
            QuestionIdCache.Enqueue(ID);
        }

        /// <summary>
        /// Speichert den aktuellen Pool an Fragen in die JSON Datei, sodass �nderungenn an Fragen beim n�chsten Spielstart verf�gbar sind.
        /// </summary>
        public static void SaveCacheToJson()
        {
            string subjectjsonstring = JsonConvert.SerializeObject(AllSubjects);
            //string leveljsonstring = JsonConvert.SerializeObject(AllPlayers);
            FileHandler.WriteExerciseJson(subjectjsonstring);
            //FileHandler.WritePlayersJson(leveljsonstring);
        }

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
        #endregion
    }
}