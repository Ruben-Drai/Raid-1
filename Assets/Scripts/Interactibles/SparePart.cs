public class SparePart : Interactible
{
    public override void Interact()
    {
        PlayerController.instance.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
    }
}
