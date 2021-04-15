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

        public static List<Exercise> getRandomExercises(Subject subject, Difficulty difficulty, int amountOfExercises)
        {
            var selectedExercises = new List<Exercise>();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(mExercisePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                var records = csv.GetRecords<Exercise>();

                for (int i = 0; i < amountOfExercises; i++)
                {
                    //Das muss noch getestet werden!!!
                    var random = new Random((int)DateTime.Now.Millisecond);
                    var sortedList = records.OrderBy(x => random.Next()).ToList();
                    int index = random.Next(sortedList.Count);
                    var randomLine = sortedList[index];
                    selectedExercises.Add(randomLine);
                }
            }

            return selectedExercises;
        }
    }
}
