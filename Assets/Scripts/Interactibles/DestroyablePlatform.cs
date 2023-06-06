using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : Interactible
{
    [SerializeField] private Sprite destroyedSprite;

    public bool isDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDestroyed && collision.gameObject.CompareTag("Crate"))
        { 
            Interact();   
        }
    }

    public override void Interact()
    {
        isDestroyed = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
