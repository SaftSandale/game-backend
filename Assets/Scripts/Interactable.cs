using PokAEmon.BackgroundWorkers;
using PokAEmon.Enums;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstrakte Klasse, von der Interaktionsklassen erben.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    #region Variables

    public int ID;
    public int NeededLevelToOpen;
    public Difficulty unlockAfterDifficulty;
    protected bool levelIsTooLow;
    protected Animator animatorLeft;
    protected Animator animatorRight;
    #endregion

    #region Unity Methods

    /// <summary>
    /// Pr�ft, ob der Spieler den Radius eines interagierbaren Objekts betritt und �ffnet das Interaktionsicon.
    /// </summary>
    /// <param name="collision">Ein 2D Collider.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().openInteractableIcon();
        }
    }

    /// <summary>
    /// Pr�ft, ob der Spieler den Radius eines interagierbaren Objekts verl�sst und schlie�t das Interaktionsicon.
    /// </summary>
    /// <param name="collision">Ein 2D Collider.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().closeInteractableIcon();
        }
    }
    #endregion

    #region Abstract Methods

    /// <summary>
    /// Interaktionsmethode, die aufgerufen wird, wenn der Spieler interagieren kann und E dr�ckt.
    /// </summary>
    public abstract void Interact();
    #endregion

    #region Methods

    /// <summary>
    /// Pr�ft, ob der Spieler mit dem aktuellen Objekt interagieren kann.
    /// </summary>
    /// <returns>Bool, ob der Spieler interagieren kann.</returns>
    protected bool CheckIfPlayerCanInteract()
    {
        bool canOpen = false;
        bool playerHasLevel = DataCache.CurrentPlayer.PlayerExperience.Level >= NeededLevelToOpen;
        if (playerHasLevel)
        {
            switch (unlockAfterDifficulty)
            {
                case Difficulty.Easy:
                    canOpen = CheckIfPlayerPassedDifficulty(DataCache.AmountCorrectEasyExercises);
                    break;
                case Difficulty.Medium:
                    canOpen = CheckIfPlayerPassedDifficulty(DataCache.AmountCorrectMediumExercises);
                    break;
                case Difficulty.Hard:
                    canOpen = CheckIfPlayerPassedDifficulty(DataCache.AmountCorrectHardExercises);
                    break;
                default:
                    canOpen = true;
                    break;
            }
            levelIsTooLow = false;
        }
        else
        {
            levelIsTooLow = true;
        }
        return canOpen;
    }

    /// <summary>
    /// Pr�ft, ob der Spieler die vorherige Schwierigkeit abgeschlossen hat.
    /// </summary>
    /// <param name="dictionary">Dictionary der vorherigen Schwierigkeit, das speichert, wieviele Aufgaben der Spieler gel�st hat.</param>
    /// <returns>Bool, ob der Spieler die vorherige Schwierigkeit abgeschlossen hat.</returns>
    private bool CheckIfPlayerPassedDifficulty(Dictionary<string, int> dictionary)
    {
        bool playerAnsweredEnoughQs = true;
        foreach (var topic in dictionary)
        {
            if (topic.Value < 10)
            {
                playerAnsweredEnoughQs = false;
            }
        }
        return playerAnsweredEnoughQs;
    }
    #endregion
}
