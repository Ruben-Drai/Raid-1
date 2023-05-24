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
        if (CurrentInteractibleObject == this) // use lever part.2
        {
            if (DoorClosed == true)
            {
                Destroy(transform.GetChild(0).gameObject);
                DoorClosed = false;
            }


            //TODO: play lever push anim
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.CompareTag("Hand")) // use lever part.1
        {
            if (PlayerController.instance.UnlockedUpgrades["Arm"] && DoorClosed && CurrentInteractibleObject == null)
            {
                CurrentInteractibleObject = this;

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand")) // use lever part.1
        {
            if (PlayerController.instance.UnlockedUpgrades["Arm"] && DoorClosed && CurrentInteractibleObject==null)
            {
                CurrentInteractibleObject = this;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Hand")) // use lever part.1
        {
            if (CurrentInteractibleObject == this) CurrentInteractibleObject = null;
        }
    }
}
