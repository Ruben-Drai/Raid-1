using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramEnter : Interactible
{
    [SerializeField] private GameObject HologramVisual, HologramBox;

    public override void Interact()
    {
        IsActivated = !IsActivated;
        HologramVisual.SetActive(IsActivated);
        HologramBox.SetActive(IsActivated);

        if (IsActivated)
        {
            FindObjectOfType<HologramManager>().Hologram();
            PlayerController.instance.Controller.SwitchCurrentActionMap("Dialogue");
        }
    }
}
