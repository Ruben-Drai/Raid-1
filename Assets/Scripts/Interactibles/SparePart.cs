using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SparePart : Interactible
{
    [SerializeField] Light2D highlight;
    public override void Interact()
    {
        IsActivated = true;
        PlayerController.UnlockedUpgrades[name] = true;
        PlayerController.instance.AvailableInteraction = null;
        gameObject.SetActive(false);
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
