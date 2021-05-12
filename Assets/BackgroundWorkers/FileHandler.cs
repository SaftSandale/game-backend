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

        public static string ReadJSON()
        {
            var jsonString = File.ReadAllText(mExercisePath);
            return jsonString;
        }

        public static void WriteJson(string jsonstring)
        {
            File.WriteAllText(mExercisePath, jsonstring);
        }
    }
}
