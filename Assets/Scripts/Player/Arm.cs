using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arm : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private GrapplingHook Hook;
    private Camera cam;

    [NonSerialized] public bool Aiming = false;
    [NonSerialized] public Vector2 Direction;
    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Aiming)
        {
            if (PlayerController.instance.Controller.currentControlScheme == "K&M")
            {
                Direction = (cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Direction), Time.deltaTime * rotationSpeed);
        }
        else if (!Hook.HasShot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.down), Time.deltaTime * rotationSpeed);
            if (transform.rotation.eulerAngles.z == Quaternion.LookRotation(Vector3.forward, Vector2.down).eulerAngles.z)
                gameObject.SetActive(false);

        }
        else if (Hook.HasShot)
        {
            Direction = new(Hook.AimPoint.x - transform.position.x, Hook.AimPoint.y - transform.position.y);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Direction);
        }


    }
}
