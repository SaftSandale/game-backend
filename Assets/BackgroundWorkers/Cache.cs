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
        private Experience PlayerLvL { get; set; }
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
            GetPlayerLvl();
        }

        private void GetAllSubjects()
        {
            List<Subject> res = JsonConvert.DeserializeObject<List<Subject>>(FileHandler.ReadPlayerLevelJSON());
            if (res != null) AllSubjects = res;
            else AllSubjects = new List<Subject>();
        }
        private void GetPlayerLvl()
        {
            Experience res = JsonConvert.DeserializeObject<Experience>(FileHandler.ReadPlayerLevelJSON());
            if (res != null) PlayerLvL = res;
            else PlayerLvL = new Experience();
        }

        public void addElement(int ID)
        {
            if(cache.Count >= maxElements)
            {
                cache.Dequeue();
            }
            cache.Enqueue(ID);
        }




        public void SaveCacheToJson()
        {
            string subjectjsonstring = JsonConvert.SerializeObject(AllSubjects);
            string leveljsonstring = JsonConvert.SerializeObject(PlayerLvL);
            FileHandler.WriteExerciseJson(subjectjsonstring);
            FileHandler.WritePlayerLevelJson(leveljsonstring);
        }
    }
}