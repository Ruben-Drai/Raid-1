using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparePart : Interactible
{
    public override void Interact()
    {
        PlayerController.instance.UnlockedUpgrades[name] = true;
        Destroy(gameObject);
    }
}
