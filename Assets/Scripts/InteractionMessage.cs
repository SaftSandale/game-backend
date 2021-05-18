using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMessage : Interactable
{
    public MessageManager messageManager;

    public override string getDescription()
    {
        throw new System.NotImplementedException();
    }

    public override void interact()
    {
        messageManager.WakeMessageBox();
    }
}
