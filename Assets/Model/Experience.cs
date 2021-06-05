using PokAEmon.Enums;

namespace PokAEmon.Model
{
    /// <summary>
    /// Model für die Erfahrungspunkte des Spielers.
    /// </summary>
    public class Experience
    {
        #region Properties

        /// <summary>
        /// Aktueller Erfahrungswert.
        /// </summary>
        public float XP { get; private set; }

        /// <summary>
        /// Aktuelles Level des Spielers.
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Erfahrungspunkte, die für das nächste Level benötigt werden.
        /// </summary>
        public float NeededXPForNextLevel { get; private set; }

        /// <summary>
        /// Bonus, der steigt, wenn der Spieler mehrere Fragen in Folge richtig beantwortet.
        /// </summary>
        public float Bonus { get; private set; }
        #endregion

        #region Contructor

        /// <summary>
        /// Konstruktor, der initial die Properties befüllt.
        /// </summary>
        public Experience()
        {
            XP = 0;
            Level = 1;
            Bonus = 1;
            NeededXPForNextLevel = 200;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Erhöht den Bonus.
        /// </summary>
        /// <param name="bonus">Bonus, der hinzugefügt werden soll.</param>
        public void SetBonus(float bonus)
        {
            Bonus += bonus;
        }

        /// <summary>
        /// Setzt den Bonus zurück.
        /// </summary>
        public void ResetBonus()
        {
            Bonus = 1;
        }

        /// <summary>
        /// Fügt Erfahrungspunkte anhand der Schwierigkeit der Frage hinzu.
        /// </summary>
        /// <param name="dif">Die Schwierigkeit der beantworteten Frage.</param>
        public void AddXPForDifficulty(Difficulty dif)
        {
            this.XP += ((int)dif * Bonus );

            NeededXPForNextLevel = Level * 100;

            if(this.XP >= NeededXPForNextLevel)
            {
                Level++;
            }
        }

        /// <summary>
        /// Fügt Erfahrungspunkte beim Tutorial hinzu.
        /// </summary>
        /// <param name="xpToAdd">Erfahrungspunkte, die hinzugefügt werden sollen.</param>
        public void AddTutorialXP(float xpToAdd)
        {
            this.XP += xpToAdd;
            if (this.XP >= NeededXPForNextLevel)
            {
                Level++;
                NeededXPForNextLevel = 300;
            }
        }
        #endregion
    }
}