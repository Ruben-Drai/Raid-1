using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramEnter : MonoBehaviour
{
    public HologramTrigger trigger;
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
            trigger.Open();
        playerController.Controller.SwitchCurrentActionMap("Dialogue");
    }
}
