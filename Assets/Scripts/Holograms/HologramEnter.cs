using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramEnter : MonoBehaviour
{
    [SerializeField] private GameObject HologramBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true && !collision.isTrigger)
        {
            HologramBox.SetActive(true);
            FindObjectOfType<HologramManager>().Hologram();
            PlayerController.instance.Controller.SwitchCurrentActionMap("Dialogue");
        }
            
    }
}
