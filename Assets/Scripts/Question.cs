/// <summary>
/// Question Script ruft Quiz UI auf, wenn der Spieler mit einem Gegenstand interagiert, der Aufgaben ausgibt.
/// </summary>
public class Question : Interactable
{
    #region Unity Variables

    public QuizManager quizManager;
    public Area area;
    #endregion

    #region Overwritten Methods

    /// <summary>
    /// Zeigt die UI für Aufgaben an.
    /// </summary>
    public override void Interact()
    {
        quizManager.WakeQuizManager(area.subject, area.topic, area.difficulty, false);
    }
    #endregion
}
