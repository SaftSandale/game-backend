/// <summary>
/// ExtendedQuestion Script ruft Quiz UI auf, wenn der Spieler mit dem Gegenstand interagiert, der benutzerdefinierte Aufgaben ausgibt.
/// </summary>
public class ExtendedQuestion : Interactable
{
    #region Unity Variables

    public QuizManager quizManager;
    #endregion

    #region Overwritten Methods

    /// <summary>
    /// Zeigt die UI für Aufgaben an.
    /// </summary>
    public override void Interact()
    {
        quizManager.WakeExtendedQuizManager();
    }
    #endregion
}
