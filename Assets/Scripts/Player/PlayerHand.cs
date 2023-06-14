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
            if (collision.transform.position.x > transform.position.x && collision.GetComponent<BigBox>()!=null)
                Player.IsAtMyLeft = false;
            else if (collision.GetComponent<BigBox>() != null)
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
