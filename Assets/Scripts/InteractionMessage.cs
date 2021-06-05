using PokAEmon.BackgroundWorkers;
using PokAEmon.Model;
using System.Linq;

/// <summary>
/// InteractionMessage Script wird aufgerufen, wenn der Spieler mit einem NPC interagiert und gibt eine Nachricht in der Textbox aus.
/// </summary>
public class InteractionMessage : Interactable
{
    #region Unity Variables

    public MessageManager messageManager;
    #endregion

    #region Overwritten Methods

    /// <summary>
    /// Wird beim Interagieren mit einem NPC aufgerufen. Ermittelt die richtige Nachricht und gibt diese aus.
    /// </summary>
    public override void Interact()
    {
        var currentTextLine = LoadCurrentTextLine();
        if (!currentTextLine.AlreadyTold)
        {
            messageManager.DisplayMessage(currentTextLine);
            if (currentTextLine.ID == 2)
            {
                DataCache.CurrentPlayer.PlayerExperience.AddTutorialXP(200);
            }
        }
        else
        {
            TextLine noInteractionText = DataCache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 5);
            messageManager.DisplayWrongInteractionMessage(noInteractionText.TextString);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Lädt die passende TextLine anhand der ID, die das Objekt, mit dem der Spieler interagiert hat.
    /// </summary>
    /// <returns>TextLine: Die aktuelle TextLine</returns>
    public TextLine LoadCurrentTextLine()
    {
        var currentTextLine = DataCache.AllTextLines.FirstOrDefault(t => t.ID == ID);
        return currentTextLine;
    }
    #endregion
}
