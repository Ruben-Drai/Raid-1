using System;
using System.Collections;
using System.Collections.Generic;
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
        foreach (var v in Enum.GetNames(typeof(GroundState)))
        {

            //for ease of debugging
            var Hit = Physics2D.Raycast(transform.position, Vector2.down, 1.74f, LayerMask.GetMask(v));
            Debug.DrawRay(Hit.point, Vector2.Perpendicular(Hit.normal).normalized, Color.blue);
            Debug.DrawRay(Hit.point, Hit.normal, Color.green);
            if (Hit == true)
            {
                controller.SlopeAdjustment = Vector2.Perpendicular(Hit.normal).normalized;
                
                Enum.TryParse(v, out groundState);
                break;
            }
        }
        //Can the player Jump ? only set if the player isn't already in a jump so that it doesn't redetect the ground at the beginning of a jump
        controller.CanJump = groundState != GroundState.Air;

        if (groundState == GroundState.Slope && !controller.IsInJump)
        {
            rb.isKinematic = !controller.IsMoving;
            rb.velocity = Vector3.zero;
        }
        else rb.isKinematic = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) //just realizing that IsInJump would be better off being called IsInAir
            controller.IsInJump = false;
    }
    
}

public enum GroundState
{
    Ground,
    Air,
    Platform,
    Slope
}