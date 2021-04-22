using LearningGame.Backend.Enums;
using LearningGame.Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.Controllers
{
    public class ExerciseController
    {
        public List<Subject> DeserializeJSON(string jsonString)
        {
            var subjects = JsonConvert.DeserializeObject<List<Subject>>(jsonString);
            return subjects;
        }
    }
}
