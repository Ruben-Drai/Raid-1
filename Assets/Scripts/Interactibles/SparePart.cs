public class SparePart : Interactible
{
    public override void Interact()
    {
        IsActivated = true;
        PlayerController.instance.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        gameObject.SetActive(false);
    }
}
