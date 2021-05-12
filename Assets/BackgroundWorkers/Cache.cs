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
            AllSubjects = DeserializeJSON(FileHandler.ReadJSON());
        }

        public void addElement(int ID)
        {
            if(cache.Count >= maxElements)
            {
                cache.Dequeue();
            }
            cache.Enqueue(ID);
        }


        public List<Subject> DeserializeJSON(string jsonString)
        {
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(jsonString);
            return subjects;
        }



        public void SaveSubjectCacheToJson()
        {
            string jsonstring = JsonConvert.SerializeObject(AllSubjects);
            FileHandler.WriteJson(jsonstring);
        }
    }
}