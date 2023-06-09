public class SparePart : Interactible
{
    public override void Interact()
    {
        IsActivated = true;
        PlayerController.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        gameObject.SetActive(false);
    }
}
