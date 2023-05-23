using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_script : MonoBehaviour
{
    public bool isCollideHand= false;
    public bool leverIsActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isCollideHand) 
        {
            leverIsActive = !leverIsActive;
            if(leverIsActive )
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            else
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            if (collision.gameObject.GetComponent<Capacity_2>().IsUnlock)
            {
                isCollideHand = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
             isCollideHand = false;
        }
    }
}
