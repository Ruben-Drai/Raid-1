using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Lever : Interactible
{
    private bool DoorClosed = true;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"] && DoorClosed == true) // use lever part.2
        {
            transform.GetChild(0).gameObject.SetActive(false);
            DoorClosed = false;
            IsActivated = true;
            //TODO: play lever push anim
        }
    }

}
