using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpener : Interactable
{
    #region Unity Variables
    public int NeededLevelToOpen;
    public MessageManager messageManager;
    private Animator animatorLeft;
    private Animator animatorRight;
    #endregion

    #region Methods
    /// <summary>
    /// Wird aufgerufen, wenn der Spieler mit einer T�r interagiert. Startet die Animation und entfernt die Kollision der T�r. Gibt au�erdem einen Fehler aus, wenn das Level nicht ausreicht.
    /// </summary>
    public override void interact()
    {
        animatorLeft = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animatorRight = gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (CheckIfPlayerCanOpenDoor())
        {
            animatorLeft.SetBool("Open", true);
            animatorRight.SetBool("Open", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            var levelTooLowTextLine = PokAEmon.BackgroundWorkers.Cache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 1);
            var textToDisplay = levelTooLowTextLine.TextString.Replace("*LEVEL*", NeededLevelToOpen.ToString());
            messageManager.DisplayWrongInteractionMessage(textToDisplay);
        }
    }

    /// <summary>
    /// �berpr�ft, ob das Spielerlevel ausreicht, um die T�r zu �ffnen.
    /// </summary>
    /// <returns>Boolean, ob der Spieler die T�r �ffnen kann.</returns>
    private bool CheckIfPlayerCanOpenDoor()
    {
        if (PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level >= NeededLevelToOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
