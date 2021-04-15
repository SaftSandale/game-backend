using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningGame.Backend.Enums;

namespace LearningGame.Backend.Model
{
    public class Subject
    {
        public Subject(string name)
        {
            SubjectName = name;
        }
        public Subject(string name, Dictionary<Difficulty, List<Exercise>> exercises)
        {
            SubjectName = name;
            Exercises = exercises;
        }


        public string SubjectName { get; set; }
        public Dictionary<Difficulty, List<Exercise>> Exercises { get; set; }


        public void FillExercises(Difficulty difficulty)
        {

        }
    }
}
