using PokAEmon.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : Interactable
{
    #region Unity Variables
    public int NeededLevelToOpen;
    public MessageManager messageManager;
    private Animator animatorLeft;
    private Animator animatorRight;
    #endregion

    private string mLevelInformationMessage = "Hmm... es scheint, dass dein Level nicht ausreicht um diese Tür zu öffnen. Du musst mindestens Level *LEVEL* sein, um das zu tun.";
    private readonly string mHardDifficultyUnlockError = "HAHA, denkst du wirklich du kommst an mir vorbei?! Da hast du dich geirrt, mich kannst du nicht besiegen!";

    #region Methods
    /// <summary>
    /// Wird aufgerufen, wenn der Spieler mit einer Tür interagiert. Startet die Animation und entfernt die Kollision der Tür. Gibt außerdem einen Fehler aus, wenn das Level nicht ausreicht.
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
            mLevelInformationMessage = mLevelInformationMessage.Replace("*LEVEL*", NeededLevelToOpen.ToString());
            messageManager.DisplayWrongInteractionMessage(mLevelInformationMessage);
        }
    }

    /// <summary>
    /// Überprüft, ob das Spielerlevel ausreicht, um die Tür zu öffnen.
    /// </summary>
    /// <returns>Boolean, ob der Spieler die Tür öffnen kann.</returns>
    private bool CheckIfPlayerCanOpenDoor()
    {
        if (Player.PlayerExperience.Level >= NeededLevelToOpen)
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
