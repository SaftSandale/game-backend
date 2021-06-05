using PokAEmon.BackgroundWorkers;
using PokAEmon.Model;
using System.Linq;
using UnityEngine;

/// <summary>
/// DoorOpener erbt von Interactable und dient dazu, Türen zu öffnen.
/// </summary>
public class DoorOpener : Interactable
{
    #region Unity Variables

    public MessageManager messageManager;
    #endregion

    #region Methods

    /// <summary>
    /// Wird aufgerufen, wenn der Spieler mit einer Tür interagiert. Startet die Animation und entfernt die Kollision der Tür. 
    /// Gibt außerdem einen Fehler aus, wenn das Level nicht ausreicht oder noch nicht genug Aufgaben gelöst wurden.
    /// </summary>
    public override void Interact()
    {
        animatorLeft = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animatorRight = gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (CheckIfPlayerCanInteract())
        {
            animatorLeft.SetBool("Open", true);
            animatorRight.SetBool("Open", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            TextLine levelTooLowTextLine = DataCache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 1);
            TextLine notAllExercisesDoneTextLine = DataCache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 4);
            string textToDisplay = string.Empty;
            if (levelIsTooLow)
            {
                textToDisplay = levelTooLowTextLine.TextString.Replace("*LEVEL*", NeededLevelToOpen.ToString());
            }
            else if (!levelIsTooLow)
            {
                textToDisplay = notAllExercisesDoneTextLine.TextString;
            }
            messageManager.DisplayWrongInteractionMessage(textToDisplay);
        }
    }
    #endregion
}
