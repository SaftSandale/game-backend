using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public abstract class Interactable : MonoBehaviour
{

    private void start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().openInteractableIcon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().closeInteractableIcon();
        }
    }

    public abstract string getDescription();
    public abstract void interact();
}
