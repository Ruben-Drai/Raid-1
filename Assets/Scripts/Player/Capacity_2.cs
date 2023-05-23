using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capacity_2 : MonoBehaviour
{
    public bool unlock = false;
    private GameObject currentBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBox != null)
        {
            if(Input.GetKey(KeyCode.E) && unlock)
            {
                currentBox.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            currentBox = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            currentBox = null;
        }
    }


    public bool IsUnlock
    {
        get { return unlock; }
        set { unlock = value; }
    }
}
