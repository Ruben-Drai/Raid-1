public class Lever : Interactible
{

    private void Update()
    {
        if (IsActivated)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Strength"]) // use lever part.2
        {
            IsActivated = true;
        }
    }

}
