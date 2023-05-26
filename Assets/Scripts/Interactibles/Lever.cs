public class Lever : Interactible
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
        if (CurrentInteractibleObject == this) // use lever part.2
        {
<<<<<<< Updated upstream
            if (DoorClosed == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                DoorClosed = false;
                IsActivated = true;
            }

=======
            IsActivated = true;
>>>>>>> Stashed changes
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
