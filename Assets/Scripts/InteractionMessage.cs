using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionMessage : Interactable
{
    #region Unity Variables
    public MessageManager messageManager;
    #endregion

    private readonly string mNoInteractionText = "Haben wir nicht eben erst gesprochen? Sieh dich etwas um, vielleicht findest du jemand anderen zum reden...";

    #region Overwritten Methods
    /// <summary>
    /// Wird beim Interagieren mit einem NPC aufgerufen. Ermittelt die richtige Nachricht und gibt diese aus.
    /// </summary>
    public override void interact()
    {
        var currentTextLine = LoadCurrentTextLine();
        if (!currentTextLine.AlreadyTold)
        {
            messageManager.DisplayMessage(currentTextLine);
            if (currentTextLine.ID == 2)
            {
                PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.AddXP(200);
            }
        }
        else
        {
            messageManager.DisplayWrongInteractionMessage(mNoInteractionText);
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
        var currentTextLine = PokAEmon.BackgroundWorkers.Cache.AllTextLines.FirstOrDefault(t => t.ID == ID);
        return currentTextLine;
    }
    #endregion
}
