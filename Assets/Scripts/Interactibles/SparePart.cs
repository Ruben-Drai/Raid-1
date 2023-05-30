public class SparePart : Interactible
{

    private void Update()
    {
        if (IsActivated) gameObject.SetActive(false);
    }
    public override void Interact()
    {
        PlayerController.instance.UnlockedUpgrades[name] = true;
        IsActivated = true;
        PlayerController.instance.AvailableInteraction = null;
    }
}
