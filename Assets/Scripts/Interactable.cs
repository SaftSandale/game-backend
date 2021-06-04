using UnityEngine;
/// <summary>
/// Abstrakte Klasse, von der Interaktionsklassen erben.
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Zahlenwert anhand dessen eine TextLine zu einem interagierbaren Objekt zugeordnet werden kann.
    /// </summary>
    public int ID;
    public bool openAfterEasy;
    public bool openAfterMedium;
    public bool openAfterHard;
    #endregion

    #region Unity Methods
    private void start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

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
    public abstract void interact();
    #endregion
}
