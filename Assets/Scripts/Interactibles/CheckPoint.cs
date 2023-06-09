using UnityEngine;

public class CheckPoint : Interactible
{
    public int drain = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsActivated && collision.GetComponent<PlayerController>() != null)
        {
            Interact();
            SaveNLoad.instance.Save();
            GameUI.instance.BatteryDrain(drain);
        }
    }
    public override void Interact()
    {
        IsActivated = true;
    }
}
