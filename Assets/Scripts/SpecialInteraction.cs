using PokAEmon.BackgroundWorkers;
using System.Linq;
using UnityEngine;

/// <summary>
/// SpecialInteraction Script verwaltet Interaktionen, die von den normalen Interaktionen mit Türen, NPCs etc abweichen.
/// </summary>
public class SpecialInteraction : Interactable
{
    #region Unity Variables

    public bool otherCriteriaNeeded;
    public bool isDoor;
    public MessageManager messageManager;
    private bool playerHasOtherCriteria;
    #endregion

    #region Methods

    /// <summary>
    /// Wird aufgerufen, wenn der Spieler mit einem besonderen Gegenstand interagiert.
    /// </summary>
    public override void Interact()
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
    /// <returns>Boolean, ob der Spieler interagieren kann.</returns>
    private bool CheckIfPlayerCanDoInteraction()
    {
        bool canOpen = false;
        if (CheckIfPlayerCanInteract())
        {
            if (otherCriteriaNeeded && playerHasOtherCriteria)
            {
                canOpen = true;
            }
            else if (!otherCriteriaNeeded)
            {
                canOpen = true;
            }
            else
            {
                canOpen = false;
            }
        }

        return canOpen;
    }

    /// <summary>
    /// Führt Interaktion durch, wenn es sich bei dem Objekt um eine Tür handelt. Entfernt Kollision der Tür und startet Animation.
    /// </summary>
    private void DoorInteraction()
    {
        animatorLeft = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animatorRight = gameObject.transform.GetChild(1).GetComponent<Animator>();

        if (CheckIfPlayerCanDoInteraction())
        {
            animatorLeft.SetBool("Open", true);
            animatorRight.SetBool("Open", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            var cannotOpenText = DataCache.AllSpecialTextLines.FirstOrDefault(t => t.ID == ID);
            messageManager.DisplayWrongInteractionMessage(cannotOpenText.TextString);
        }
    }

    /// <summary>
    /// Führt Interaktion durch, wenn es sich bei dem Objekt um keine Tür handelt. Entfernt Kollision des Gegenstands und blendet ihn aus.
    /// </summary>
    private void OtherInteraction()
    {
        if (CheckIfPlayerCanDoInteraction())
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            var cannotOpenText = DataCache.AllSpecialTextLines.FirstOrDefault(t => t.ID == ID);
            messageManager.DisplayWrongInteractionMessage(cannotOpenText.TextString);
        }
    }
    #endregion
}
