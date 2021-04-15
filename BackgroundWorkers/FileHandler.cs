using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningGame.Backend.BackgroundWorkers
{
    public class FileHandler
    {
        private static readonly string mBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string mExercisePath = Path.Combine(mBaseDirectory, @"\Exercises");


    }
}
