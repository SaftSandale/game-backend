using PokAEmon.Enums;

namespace PokAEmon.Model
{
    /// <summary>
    /// Model für alle Daten des Spielers.
    /// </summary>
    public class Player
    {
        #region Properties

        /// <summary>
        /// Name des Spielers.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Erfahrung des Spielers
        /// </summary>
        public Experience PlayerExperience { get; set; }

        /// <summary>
        /// QuestionCache, der speichert, ob die letzten Aufgaben richtig beantwortet wurden.
        /// </summary>
        private QuestionCache QC { get; set; }
        #endregion

        #region Internal Class
        /// <summary>
        /// Interne Klasse QuestionCache
        /// </summary>
        internal class QuestionCache
        {
            #region Properties

            /// <summary>
            /// Anzahl der in Folge richtig beantworteten Fragen.
            /// </summary>
            public int Index { get; private set; }

            /// <summary>
            /// Anzahl der Fragen, die in Folge richtig beantwortet werden müssen, um einen Bonus zu erhalten.
            /// </summary>
            private int BonusLimit { get; set; }
            #endregion

            #region Constructor

            /// <summary>
            /// Konstruktor, der Index und BonusLimit befüllt.
            /// </summary>
            /// <param name="limit">Anzahl der Fragen, die in Folge richtig beantwortet werden müssen, um einen Bonus zu erhalten.</param>
            public QuestionCache(int limit)
            {
                Index = 0;
                BonusLimit = limit;
            }
            #endregion

            #region Methods

            /// <summary>
            /// Zählt den Index für jede richtige Frage hoch oder setzt ihn auf null, wenn eine Falsche Antwort ausgewählt wurde.
            /// </summary>
            /// <param name="question">Boolean, ob die Frage richtig oder falsch beantwortet wurde.</param>
            public void AddElement(bool question)
            {
                if (question == true)
                {
                    Index++;
                }
                else
                {
                    Index = 0;
                }
            }

            /// <summary>
            /// Berechnet den aktuellen Bonus.
            /// </summary>
            /// <returns>Gibt den aktuellen Bonus zurück.</returns>
            public int CalcBonus()
            {
                return (Index % BonusLimit);
            }
            #endregion
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Instanziiert einen Spieler und befüllt die Properties.
        /// </summary>
        /// <param name="name">Name des Spielers.</param>
        /// <param name="xp">Erfahrungspunkte des Spielers.</param>
        public Player(string name, Experience xp)
        {
            PlayerName = name;
            PlayerExperience = xp;
            QC = new QuestionCache(10);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Passt die Erfahrung anhand der Schwierigkeit an und setzt den Bonus.
        /// </summary>
        /// <param name="dif">Schwierigkeit der Frage.</param>
        /// <param name="answerStatus">Boolean, ob die Frage richtig beantwortet wurde.</param>
        public void UpdateXP(Difficulty dif, bool answerStatus)
        {
            QC.AddElement(answerStatus);
            if(QC.CalcBonus() == 0)
            {
                PlayerExperience.ResetBonus();
            }
            else
            {
                PlayerExperience.SetBonus((float)(0.05 * QC.CalcBonus()));
            }

            if (answerStatus)
            {
                PlayerExperience.AddXPForDifficulty(dif);
            }
        }
        #endregion
    }
}
