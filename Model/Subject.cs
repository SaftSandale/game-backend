using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningGame.Backend.Enums;

namespace LearningGame.Backend.Model
{
    class Subject
    {
        public string SubjectName { get; set; }
        public Dictionary<Difficulty, List<Exercise>> Exercises { get; set; }

        public void FillExercises(Difficulty difficulty)
        {

        }
    }
}
