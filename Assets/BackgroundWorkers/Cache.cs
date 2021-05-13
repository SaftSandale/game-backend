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
        private int maxElements { get; set; }
        private static Queue<int> cache { get; set; }
        //private List<Player> AllPlayers { get; set; }
        //public Player CurrentPlayer { get; set; }
        public static List<Subject> AllSubjects { get; set; }
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
                            if (!cache.Contains(ex.ID))
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

        public Cache(int anzElements)
        {
            maxElements = anzElements;
            cache = new Queue<int>();
            GetAllSubjects();
            //GetAllPlayers();
        }

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

        public void addElement(int ID)
        {
            if(cache.Count >= maxElements)
            {
                cache.Dequeue();
            }
            cache.Enqueue(ID);
        }

        //public void SaveCacheToJson()
        //{
        //    string subjectjsonstring = JsonConvert.SerializeObject(AllSubjects);
        //    string leveljsonstring = JsonConvert.SerializeObject(AllPlayers);
        //    FileHandler.WriteExerciseJson(subjectjsonstring);
        //    FileHandler.WritePlayersJson(leveljsonstring);
        //}
    }
}