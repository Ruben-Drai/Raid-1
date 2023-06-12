using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramEnter : Interactible
{
    [SerializeField] private GameObject HologramVisual, HologramBox;

    public override void Interact()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true && !collision.isTrigger)
        {
            HologramVisual.SetActive(true);
            HologramBox.SetActive(true);
            FindObjectOfType<HologramManager>().Hologram();
            PlayerController.instance.Controller.SwitchCurrentActionMap("Dialogue");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HologramVisual.SetActive(false);
        HologramBox.SetActive(false);
    }
}
