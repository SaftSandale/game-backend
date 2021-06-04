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
    private bool levelTooLow;
    #endregion

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
            var levelTooLowTextLine = PokAEmon.BackgroundWorkers.Cache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 1);
            var notAllExercisesDoneTextLine = PokAEmon.BackgroundWorkers.Cache.AllSpecialTextLines.FirstOrDefault(t => t.ID == 4);
            string textToDisplay = "";
            if (levelTooLow)
            {
                textToDisplay = levelTooLowTextLine.TextString.Replace("*LEVEL*", NeededLevelToOpen.ToString());
            }
            else if (!levelTooLow)
            {
                textToDisplay = notAllExercisesDoneTextLine.TextString;
            }
            messageManager.DisplayWrongInteractionMessage(textToDisplay);
        }
    }

    /// <summary>
    /// Überprüft, ob das Spielerlevel ausreicht, um die Tür zu öffnen.
    /// </summary>
    /// <returns>Boolean, ob der Spieler die Tür öffnen kann.</returns>
    private bool CheckIfPlayerCanOpenDoor()
    {
        bool canOpen = false;
        if (PokAEmon.BackgroundWorkers.Cache.CurrentPlayer.PlayerExperience.Level >= NeededLevelToOpen)
        {
            if (openAfterEasy)
            {
                bool playerAnswered50EzQs = true;
                foreach (var topic in PokAEmon.BackgroundWorkers.Cache.AmountCorrectEasyExercises)
                {
                    if (topic.Value < 10)
                    {
                        playerAnswered50EzQs = false;
                    }
                }
                canOpen = playerAnswered50EzQs;
            }
            else if (openAfterMedium)
            {
                bool playerAnswered50EzQs = true;
                foreach (var topic in PokAEmon.BackgroundWorkers.Cache.AmountCorrectMediumExercises)
                {
                    if (topic.Value < 10)
                    {
                        playerAnswered50EzQs = false;
                    }
                }
                canOpen = playerAnswered50EzQs;
            }
            else if (openAfterHard)
            {
                bool playerAnswered50EzQs = true;
                foreach (var topic in PokAEmon.BackgroundWorkers.Cache.AmountCorrectHardExercises)
                {
                    if (topic.Value < 10)
                    {
                        playerAnswered50EzQs = false;
                    }
                }
                canOpen = playerAnswered50EzQs;
            }
            else if(!openAfterMedium && !openAfterEasy)
            {
                canOpen = true;
            }
            levelTooLow = false;
        }
        else
        {
            levelTooLow = true;
        }
        return canOpen;
    }
    #endregion
}
