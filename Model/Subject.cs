using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningGame.Backend.Enums;

namespace LearningGame.Backend.Model
{
    public class Subject
    {
        public string SubjectName { get; set; }
        public Dictionary<Difficulty, List<Exercise>> Exercises { get; set; }

        public void FillRandomExercises(Difficulty difficulty)
        {
            
        }
    }
}
