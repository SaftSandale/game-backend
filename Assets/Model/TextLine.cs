namespace PokAEmon.Model
{
    /// <summary>
    /// Model für Nachrichten, die  im Spiel von NPCs ausgegeben werden.
    /// </summary>
    public class TextLine
    {
        #region Properties
        /// <summary>
        /// Ein Zahlenwert, um alle Nachrichten unverwechselbar zu unterscheiden.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Beinhaltet den Text einer Nachricht.
        /// </summary>
        public string TextString { get; set; }

        /// <summary>
        /// Gibt an, ob eine Nachricht bereits ausgegeben wurde. Standardmäßig auf false gesetzt, da erst nach dem Spielstart Nachrichten ausgegeben werden.
        /// Dies spart Speicherplatz in der JSON Datei.
        /// </summary>
        public bool AlreadyTold { get; set; } = false;
        #endregion
    }
}
