using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private LineRenderer line;
    [SerializeField] private DistanceJoint2D joint;


    [NonSerialized] public Vector2 AimPoint;
    [NonSerialized] public bool HasShot;
    [NonSerialized] public bool Hooked;
    // Start is called before the first frame update
    void Start()
    {
        joint.enabled = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.enabled)
        {
            line.SetPosition(1, transform.position);
        }
    }
    public void ActivateHook()
    {
        gameObject.SetActive(true);
    }
    public void FireHook()
    {
        if (PlayerController.instance.Controller.currentControlScheme == "K&M")
            AimPoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        line.SetPosition(0, AimPoint);
        line.SetPosition(1, transform.position);
        joint.connectedAnchor = AimPoint;
        joint.enabled = true;
        line.enabled = true;
        HasShot = true;
        Hooked = true;
    }
    public void ReturnHook()
    {
        joint.enabled = false;
        line.enabled = false;
        HasShot = false;
        Hooked = false;
    }
}
