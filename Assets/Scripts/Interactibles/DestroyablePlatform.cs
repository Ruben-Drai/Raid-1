using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : Interactible
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActivated && collision.gameObject.CompareTag("Crate"))
        {
            Interact();
        }
    }
    public override void Interact()
    {
        IsActivated = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
