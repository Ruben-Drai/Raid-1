public class Lever : Interactible
{

    private void Update()
    {
        if (IsActivated && !transform.Find("door").gameObject.activeSelf)
        {
            transform.Find("door").gameObject.SetActive(true);
            IsActivated = false;
        }
        else if (IsActivated && transform.Find("door").gameObject.activeSelf)
        {
            transform.Find("door").gameObject.SetActive(false);
            IsActivated = false;
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
