using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.BackgroundWorkers
{
    public class FileHandler
    {
        private static readonly string mBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string mExercisePath = /*Path.Combine(mBaseDirectory, @"\Exercises")*/ @"D:\Documents\Schule\JavaProjekt\PokAEmon\Assets\Exercises\Exercises1.JSON";

        public static string ReadJSON()
        {
            var jsonString = File.ReadAllText(mExercisePath);
            return jsonString;
        }
    }
}
