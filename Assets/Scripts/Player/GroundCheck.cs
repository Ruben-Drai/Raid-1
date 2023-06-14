using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private PlayerController controller;
    public GroundState groundState;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        controller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundState = GroundState.Air;
        controller.SlopeAdjustment = -Vector2.one;
        bool InteractibleIsTrigger = false;
        RaycastHit2D Hit = new();

        //finds what's under the player, from the layer of the object under it, if none, then it's air by default
        foreach (var v in Enum.GetNames(typeof(GroundState)))
        {
            //raycasts under player towards the ground from the feet pos and gets layer from it and sets it as the current state that has the same name in the Enum
            Hit = Physics2D.Raycast(transform.position, Vector2.down, 2.5f * (controller.transform.lossyScale.x / 3), LayerMask.GetMask(v));
            if (Hit == true)
            {
                controller.SlopeAdjustment = Vector2.Perpendicular(Hit.normal).normalized;
                InteractibleIsTrigger = Hit.collider.isTrigger;
                Enum.TryParse(v, out groundState);
                break;
            }
        }
        //Can the player Jump ? only set if the player isn't already in a jump so that it doesn't redetect the ground at the beginning of a jump
        controller.CanJump = groundState == GroundState.Interactibles && InteractibleIsTrigger ? false : groundState != GroundState.Air;

        //Freezes player if on slope and unmoving
        if (groundState == GroundState.Slope && !controller.IsInJump)
        {
            rb.isKinematic = !controller.IsMoving;
            rb.velocity = Vector3.zero;
        }
        else rb.isKinematic = false;


        //"Sticks" the player to the platform if the player is on a platform
        if (groundState == GroundState.Platform && !controller.hook.HasShot && !controller.IsInJump && transform.parent.parent == null)
        {
            transform.parent.SetParent(Hit.collider.transform);
            controller.rb.interpolation = RigidbodyInterpolation2D.None;
        }
        else if (groundState != GroundState.Platform && transform.parent.parent != null)
        {
            transform.parent.SetParent(null);
            controller.rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger) //just realizing that IsInJump would be better off being called IsInAir
        {
            controller.IsInJump = false;
            controller.CanDoubleJump = true;
            if(controller.IsInJump)
            {
                GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
                Debug.Log("SoundPlaying");
            }
            
        }

    }

}

public enum GroundState
{
    Ground,
    Air,
    Platform,
    Slope,
    Interactibles
}