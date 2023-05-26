
public class SparePart : Interactible
{
<<<<<<< Updated upstream
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
=======
    private void Update()
    {
        if(IsActivated) gameObject.SetActive(false);
>>>>>>> Stashed changes
    }
    public override void Interact()
    {
        PlayerController.instance.UnlockedUpgrades[name] = true;
<<<<<<< Updated upstream
        CurrentInteractibleObject = null;
        Destroy(gameObject);
=======
        IsActivated = true;
        PlayerController.instance.AvailableInteraction = null;
>>>>>>> Stashed changes
    }
}
