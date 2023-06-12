using UnityEngine;

public class SparePart : Interactible
{
    public DialogueTrigger dialogue;
    public override void Interact()
    {
        IsActivated = true;
        PlayerController.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        gameObject.SetActive(false);
        dialogue?.gameObject.SetActive(true);
        dialogue?.StartDialogue();
    }
}
