using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    private bool DoorClosed = true;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["ArmGun"] && DoorClosed)
        {
            transform.localScale /= 2;
            Destroy(transform.GetChild(0).gameObject);
            DoorClosed = false;
            IsActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerFist"))
        {
            Interact();
        }
    }
}
