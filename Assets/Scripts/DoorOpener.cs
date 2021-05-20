using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : Interactable
{
    private Animator animatorLeft;
    private Animator animatorRight;
    public override string getDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void interact()
    {
        animatorLeft = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animatorRight = gameObject.transform.GetChild(1).GetComponent<Animator>();

        animatorLeft.SetBool("Open", true);
        animatorRight.SetBool("Open", true);

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
