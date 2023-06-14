using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SparePart : Interactible
{    
    public DialogueTrigger dialogue;
    public BoxCollider2D door;
    [SerializeField] Light2D highlight;
    public override void Interact()
    {
        IsActivated = !IsActivated;
        PlayerController.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        GetComponent<BoxCollider2D>().enabled = !IsActivated;
        gameObject.GetComponent<SpriteRenderer>().enabled = !IsActivated;
        gameObject.SetActive(!IsActivated);
        if(door != null)
            door.enabled = !IsActivated;

        dialogue?.gameObject.SetActive(!IsActivated);
        dialogue?.StartDialogue();
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            highlight.intensity = 1.39f;
        }
    }
    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            highlight.intensity = 0;
        }
    }
}
