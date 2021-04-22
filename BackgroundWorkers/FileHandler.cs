using CsvHelper;
using CsvHelper.Configuration;
using LearningGame.Backend.Enums;
using LearningGame.Backend.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public string ReadJSON(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            return jsonString;
        }
    }
}
