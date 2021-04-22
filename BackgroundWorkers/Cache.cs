using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.BackgroundWorkers
{
    public class Cache
    {
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
            foreach(Object ob in cache)
            {
                if(ob == ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}