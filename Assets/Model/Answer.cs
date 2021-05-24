using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokAEmon.Model
{
    public class Answer
    {
        #region Properties
        /// <summary>
        /// Text der Antwort.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Boolean, ob die Antwort richtig ist.
        /// </summary>
        public bool isCorrect { get; set; }
        #endregion

        #region Konstrukor
        /// <summary>
        /// Konstruktor, der Properties mit übergebenen Werten befüllt.
        /// </summary>
        /// <param name="text">Text der Antwort.</param>
        /// <param name="iscorrect">Boolean, ob die Antwort korrekt ist.</param>
        public Answer(string text, bool iscorrect)
        {
            Text = text;
            isCorrect = iscorrect;
        }
        #endregion
    }
}
