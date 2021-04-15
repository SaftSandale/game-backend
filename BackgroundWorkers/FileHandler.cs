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

        //Lädt aktuell alle Aufgaben und nicht nur random Aufgaben.
        public IEnumerable<Exercise> fillExerciseList(Subject subject, Difficulty difficulty)
        {
            IEnumerable<Exercise> records;
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(mExercisePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                records = csv.GetRecords<Exercise>();
            }

            return records;
        }
    }
}
