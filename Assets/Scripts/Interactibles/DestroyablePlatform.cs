using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : Interactible
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActivated && collision.gameObject.CompareTag("Crate"))
        { 
            IsActivated = true;
        }
    }
    private void Update()
    {
        if(IsActivated && transform.GetChild(0).gameObject.activeSelf)
        {
            Interact();
        }
    }
    public override void Interact()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
