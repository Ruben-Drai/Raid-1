using UnityEngine;

public class SparePart : Interactible
{
    public DialogueTrigger dialogue;
    public override void Interact()
    {
        IsActivated = true;
        PlayerController.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dialogue?.gameObject.SetActive(true);
        dialogue?.StartDialogue();
    }
}
