using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private PlayerController Player;

    private void Start()
    {
        Player = transform.parent.GetComponent<PlayerController>();
    }

    //gets interactible from trigger box around the player
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null)
        {
            Player.AvailableInteraction = collision.GetComponent<Interactible>();
            Player.AvailableInteraction.transform.Find("Highlight")?.gameObject.SetActive(true); // Activates highlighting when the player is close by
            if (collision.transform.position.x > transform.position.x)
                Player.IsAtMyLeft = false;
            else
                Player.IsAtMyLeft = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null
            && Player.AvailableInteraction == collision.GetComponent<Interactible>()
            && Vector2.Distance(transform.position, collision.transform.position) > 0.5f)
        {
            Player.AvailableInteraction.transform.Find("Highlight")?.gameObject.SetActive(false); // Deactivates highlighting when player moves away.
            Player.AvailableInteraction = null;
        }
    }
}
