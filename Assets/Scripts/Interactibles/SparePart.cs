using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePart : Interactible
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (CurrentInteractibleObject == null)
            {
                CurrentInteractibleObject = this;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (CurrentInteractibleObject == null)
            {
                CurrentInteractibleObject = this;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (CurrentInteractibleObject == this) CurrentInteractibleObject = null;
        }
    }
    public override void Interact()
    {
        PlayerController.instance.UnlockedUpgrades[name] = true;
        CurrentInteractibleObject = null;
        Destroy(gameObject);
    }
}
