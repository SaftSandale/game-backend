namespace PokAEmon.Model
{
    /// <summary>
    /// Model für alle Antworten.
    /// </summary>
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
        public bool IsCorrect { get; set; }
        #endregion

        #region Contructor

        /// <summary>
        /// Konstruktor, der Properties mit übergebenen Werten befüllt.
        /// </summary>
        /// <param name="text">Text der Antwort.</param>
        /// <param name="isCorrect">Boolean, ob die Antwort korrekt ist.</param>
        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
        #endregion
    }
}
