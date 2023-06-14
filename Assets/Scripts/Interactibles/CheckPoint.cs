using UnityEngine;

public class CheckPoint : Interactible
{
    public int drain = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* When the player touches a collision it will save interactibles position and drain his battery a little */
        if(!IsActivated && collision.GetComponent<PlayerController>() != null)
        {
            Interact();
            SaveNLoad.instance.StartCoroutine(SaveNLoad.instance.SaveRoutine());
            GameUI.instance.BatteryDrain(drain);
        }
    }
    public override void Interact()
    {
        IsActivated = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
