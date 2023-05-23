using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_script : MonoBehaviour
{
    public float lockMass = 100000;
    public float unlockMass = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hand")
        {
           if(collision.gameObject.GetComponent<Capacity_2>().IsUnlock)
            {
                gameObject.GetComponent<Rigidbody2D>().mass = unlockMass;
            }
            else
                gameObject.GetComponent<Rigidbody2D>().mass = lockMass;
            Debug.Log("collIsEnter");
        }
       
    }
}
