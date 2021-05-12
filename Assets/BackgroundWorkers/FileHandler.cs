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
        private static readonly string mPlayerLevelPath = mBaseDirectory + "/SaveStates/PlayerLevel.JSON";

        public static string ReadExerciseJSON()
        {
            var jsonString = File.ReadAllText(mExercisePath);
            return jsonString;
        }

        public static void WriteExerciseJson(string jsonstring)
        {
            File.WriteAllText(mExercisePath, jsonstring);
        }


        public static string ReadPlayerLevelJSON()
        {
            var jsonString = File.ReadAllText(mPlayerLevelPath);
            return jsonString;
        }

        public static void WritePlayerLevelJson(string jsonstring)
        {
            File.WriteAllText(mPlayerLevelPath, jsonstring);
        }
    }
}
