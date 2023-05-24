using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBox : Interactible
{
    [SerializeField] private float lockedMass = 100000,
                                  unlockedMass = 10;
    private Rigidbody2D rb;

    private bool IsBeingLifted = false;
    // Start is called before the first frame update
    void Start()
    {  
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.mass == unlockedMass)
        {
            rb.velocity = PlayerController.instance.GetComponent<Rigidbody2D>().velocity;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            if (PlayerController.instance.UnlockedUpgrades["Arm"] && CurrentInteractibleObject == null)
            {
                CurrentInteractibleObject = this;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (PlayerController.instance.UnlockedUpgrades["Arm"] && CurrentInteractibleObject == null)
            {
                CurrentInteractibleObject = this;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            rb.mass = lockedMass;
            if (CurrentInteractibleObject == this)
                CurrentInteractibleObject = null;
        }
    }

    public override void Interact()
    {
        if (!IsBeingLifted)
        {
            rb.mass = unlockedMass;
            PlayerController.instance.IsPushingBox = true;
            IsBeingLifted = true;
        }
        else
        {
            rb.mass = lockedMass;
            IsBeingLifted = false;
            PlayerController.instance.IsPushingBox = false;
        }
    }
}
