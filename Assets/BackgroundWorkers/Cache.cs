using Newtonsoft.Json;
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
        /// Speichert eine Liste aller Nachrichten, die im Spiel ausgegeben werden können.
        /// </summary>
        public static List<TextLine> AllTextLines { get; set; }
        /// <summary>
        /// Speichert eine Liste aller besonderen Nachrichten, die im Spiel ausgegeben werden können.
        /// </summary>
        public static List<TextLine> AllSpecialTextLines { get; set; }
        /// <summary>
        /// Speichert eine Liste alle Fächer mit Fragen, die im Spiel gestellt werden können.
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
        #endregion

        #region Constructor
        /// <summary>
        /// Konstruktor, der die Anzahl der zu Speichernden Elemente setzt und weitere Properties befüllt.
        /// </summary>
        /// <param name="anzElements">Maximale Anzahl, die der Cache speichern soll.</param>
        public Cache(int anzElements)
        {
            maxElements = anzElements;
            QuestionIdCache = new Queue<int>();
            GetAllSubjects();
            GetAllTextLines();
            GetAllSpecialTextLines();
            //GetAllPlayers();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Deserialisiert JSON String mit allen Fächern und Aufgaben in das Subject Model und befüllt die Property AllSubjects mit diesen Daten.
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
        /// Deserialisiert JSON String mit allen Nachrichten, die im Spiel ausgegeben werden können in das TextLine Model und befüllt die Property AllTextLines mit diesen Daten.
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
        /// Fügt der Property QuestionIdCache eine ID einer Frage hinzu. Sollte maxElements überschritten werden, wird der letzte Eintrag aus dem QuestionIdCache entfernt.
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
        /// Speichert den aktuellen Pool an Fragen in die JSON Datei, sodass Änderungenn an Fragen beim nächsten Spielstart verfügbar sind.
        /// </summary>
        public static void SaveCacheToJson()
        {
            string subjectjsonstring = JsonConvert.SerializeObject(AllSubjects);
            //string leveljsonstring = JsonConvert.SerializeObject(AllPlayers);
            FileHandler.WriteExerciseJson(subjectjsonstring);
            //FileHandler.WritePlayersJson(leveljsonstring);
        }
        #endregion
    }
}