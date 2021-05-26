using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpecialInteraction : Interactable
{
    #region Unity Variables
    public int NeededLevelToOpen;
    public bool otherCriteriaNeeded;
    public bool isDoor;
    
    public MessageManager messageManager;
    private Animator animatorLeft;
    private Animator animatorRight;
    private bool playerHasOtherCriteria;
    #endregion

    #region Methods
    /// <summary>
    /// Wird aufgerufen, wenn der Spieler mit einer Tür interagiert. Startet die Animation und entfernt die Kollision der Tür. Gibt außerdem einen Fehler aus, wenn das Level nicht ausreicht.
    /// </summary>
    public override void interact()
    {
        if (isDoor)
        {
            DoorInteraction();
        }
        else if (!isDoor)
        {
            OtherInteraction();
        }
    }

    /// <summary>
    /// Überprüft, ob der Spieler alle Anforderungen erfüllt, um zu interagieren.
    /// </summary>
    /// <returns>Boolean, ob der Spieler die Tür öffnen kann.</returns>
    private bool CheckIfPlayerCanOpenDoor()
    {
        var levelIsHighEnough = PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level >= NeededLevelToOpen;
        if (levelIsHighEnough && otherCriteriaNeeded && playerHasOtherCriteria)
        {
            return true;
        }
        else if (levelIsHighEnough && !otherCriteriaNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DoorInteraction()
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
            var cannotOpenText = PokAEmon.BackgroundWorkers.Cache.AllSpecialTextLines.FirstOrDefault(t => t.ID == ID);
            messageManager.DisplayWrongInteractionMessage(cannotOpenText.TextString);
        }
    }

    private void OtherInteraction()
    {
        if (CheckIfPlayerCanOpenDoor())
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            var cannotOpenText = PokAEmon.BackgroundWorkers.Cache.AllSpecialTextLines.FirstOrDefault(t => t.ID == ID);
            messageManager.DisplayWrongInteractionMessage(cannotOpenText.TextString);
        }
    }
    #endregion
}
