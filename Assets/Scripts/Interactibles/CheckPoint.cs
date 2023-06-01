using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool used = false;
    private int drain = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!used && collision.GetComponent<PlayerController>() != null)
        {
            Debug.Log("checkpoint");
            used = true;
            GameUI.instance.BatterytDrain(drain);
        }
    }
}
