//using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PokAEmon.BackgroundWorkers
{
    public class FileHandler
    {
        private static  string mBaseDirectory = Application.dataPath;
        private static readonly string mExercisePath = mBaseDirectory + "/Exercises/Exercises1.JSON";
        private static readonly string mPlayersPath = mBaseDirectory + "/SaveStates/Players.JSON";
        private static readonly string mTextLinesPath = mBaseDirectory + "/Text/InteractionTexts.csv";

        public static string ReadExerciseJSON()
        {
            var jsonString = File.ReadAllText(mExercisePath);
            return jsonString;
        }

        public static void WriteExerciseJson(string jsonstring)
        {
            File.WriteAllText(mExercisePath, jsonstring);
        }

        public static string ReadPlayersJSON()
        {
            var jsonString = File.ReadAllText(mPlayersPath);
            return jsonString;
        }

        public static void WritePlayersJson(string jsonstring)
        {
            File.WriteAllText(mPlayersPath, jsonstring);
        }

        //public static string ReadTextLinesCSV()
        //{
        //    using (TextReader reader = File.OpenText(mTextLinesPath))
        //    {
        //        CsvReader csvReader = new CsvReader(reader);
        //    }
        //}
    }
}
