using UnityEngine;

public class Button : Interactible
{
    private bool DoorClosed = true;

    private void Update()
    {
        if (IsActivated)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            DoorClosed = false;
        }
    }
    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["ArmGun"] && DoorClosed)
        {
            IsActivated = true;
            transform.localScale /= 2;
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
