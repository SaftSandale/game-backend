using System.IO;
using UnityEngine;

namespace PokAEmon.BackgroundWorkers
{
    /// <summary>
    /// FileHandler ist die Klasse, die Inhalte von Dateien ausliest und verändert.
    /// </summary>
    public class FileHandler
    {
        #region Path Variables

        /// <summary>
        /// Speichert einen eindeutigen Pfad zum StreamingAssets Ordner des Projekts.
        /// </summary>
        private static  string mBaseDirectory = Application.streamingAssetsPath;

        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle Afugaben beinhaltet.
        /// </summary>
        private static readonly string mExercisePath = mBaseDirectory + "/Exercises/Exercises.JSON";

        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle TextLines speichert.
        /// </summary>
        private static readonly string mTextLinesPath = mBaseDirectory + "/Texts/InteractionTexts.JSON";

        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle besonderen TextLines speichert.
        /// </summary>
        private static readonly string mSpecialTextLinesPath = mBaseDirectory + "/Texts/SpecialInteractions.JSON";
        #endregion

        #region Methods

        /// <summary>
        /// Liest die JSON Datei aus, die die Aufgaben beinhaltet.
        /// </summary>
        /// <returns>Den Inhalt der JSON Datei als String.</returns>
        public static string ReadExerciseJSON()
        {
            var jsonString = File.ReadAllText(mExercisePath);
            return jsonString;
        }

        /// <summary>
        /// Schreibt die aktuellen Aufgaben aus dem Cache in die JSON Datei.
        /// </summary>
        /// <param name="jsonstring">Einen JSON String, der in die Datei geschrieben werden soll.</param>
        public static void WriteExerciseJson(string jsonstring)
        {
            File.WriteAllText(mExercisePath, jsonstring);
        }

        /// <summary>
        /// Liest die JSON Datei aus, die die TextLines beinhaltet.
        /// </summary>
        /// <returns>Den Inhalt der Datei als String.</returns>
        public static string ReadTextLineJSON()
        {
            var jsonString = File.ReadAllText(mTextLinesPath);
            return jsonString;
        }

        /// <summary>
        /// Liest die JSON Datei aus, die die besonderen TextLines beinhaltet.
        /// </summary>
        /// <returns>Den Inhalt der Datei als String.</returns>
        public static string ReadSpecialTextLineJSON()
        {
            var jsonString = File.ReadAllText(mSpecialTextLinesPath);
            return jsonString;
        }
        #endregion
    }
}
