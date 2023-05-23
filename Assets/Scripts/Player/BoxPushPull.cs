using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPushPull : MonoBehaviour
{
    private bool unlocked = true;
    public GameObject currentBox;


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


    public bool IsUnlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }
}
