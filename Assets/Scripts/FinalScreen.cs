/// <summary>
/// Wird aufgerufen, wenn der Spieler das Spiel geschafft hat und mit dem letzten Gegenstand interagiert.
/// </summary>
public class FinalScreen : Interactable
{
    #region Unity Variables

    public InformationUIManager informationUIManager;
    #endregion

    #region Methods
    public override void Interact()
    {
        informationUIManager.WakeEndScreen();
    }
    #endregion
}
