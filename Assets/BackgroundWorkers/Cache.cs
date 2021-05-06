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
        public static List<Subject> AllSubjects { get; set; }
        private int maxElements { get; set; }
        private Queue<int> cache { get; set; }

        Cache(int anzElements)
        {
            maxElements = anzElements;
            cache = new Queue<int>();
        }

        public void addElement(int ID)
        {
            if(cache.Count >= maxElements)
            {
                cache.Dequeue();
            }
            cache.Enqueue(ID);
        }

        public bool checkContains(int ID)
        {
            return cache.Contains(ID);
        }
    }
}