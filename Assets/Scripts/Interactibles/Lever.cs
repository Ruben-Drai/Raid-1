public class Lever : Interactible
{

    private void Update()
    {
        if (IsActivated)
        {
            transform.Find("door").gameObject.SetActive(false);
        }
    }
    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"]) // use lever part.2
        {
            IsActivated = true;
        }
    }

}
