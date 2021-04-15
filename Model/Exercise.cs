using LearningGame.Controllers.Enums;
using LearningGame.Controllers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Model
{
    public class Exercise
    {
        public string ExerciseText { get; set; }
        public List<Answer> Answers { get; set; }
        public Difficultys Difficulty { get; set; }
        public Subjects Subject { get; set; }
    }
}
