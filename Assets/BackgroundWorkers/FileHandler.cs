﻿using System;
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
        #region Private Variables
        /// <summary>
        /// Speichert den Dateipfad zum Spiel.
        /// </summary>
        private static  string mBaseDirectory = Application.streamingAssetsPath;
        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle Exercises beinhaltet.
        /// </summary>
        private static readonly string mExercisePath = mBaseDirectory + "/Exercises/Exercises.JSON";
        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle Spielerdaten speichert.
        /// </summary>
        private static readonly string mPlayersPath = mBaseDirectory + "/SaveStates/Players.JSON";
        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle TextLines speichert.
        /// </summary>
        private static readonly string mTextLinesPath = mBaseDirectory + "/Texts/InteractionTexts.JSON";
        /// <summary>
        /// Speichert den Pfad zur JSON Datei, die alle speziellen TextLines speichert.
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
        /// Liest die JSON Datei aus, die die Spielerdaten beinhaltet.
        /// </summary>
        /// <returns>Den Inhalt der JSON Datei als String.</returns>
        public static string ReadPlayersJSON()
        {
            var jsonString = File.ReadAllText(mPlayersPath);
            return jsonString;
        }

        /// <summary>
        /// Schreibt die aktuellen Spielerdaten aus dem Cache in die JSON Datei.
        /// </summary>
        /// <param name="jsonstring">Einen JSON String, der in die Datei geschrieben werden soll.</param>
        public static void WritePlayersJson(string jsonstring)
        {
            File.WriteAllText(mPlayersPath, jsonstring);
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
