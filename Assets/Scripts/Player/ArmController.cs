using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ArmController : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject fist;
    [SerializeField] private Camera cam;

    [Header("Arm")]
    private Quaternion rotation;
    private Quaternion currentRotation;
    private Quaternion originalRotation = Quaternion.Euler(0, 0, -90);

    public bool canRotate = false; //If the arm can rotate
    public bool LimitMovement { get { return playerController.IsHunging || playerController.ReturnGrappleToInitialPosition ? false : canRotate || shooting; } }

    private bool onHold = false; //If the fire button is currently holded
    private bool flipped = false; //If the player's sprite is flipped
    public bool shooting = false; //If the player is currently shooting the fist
    public bool returning; //If the fist is currently returning to the arm
    public bool isReturnToOrigin = true; //If the fist is totally retun to is original position

    private float rotationSpeed = 12f;
   
    private Vector3 cursorDirection;
    private Vector3 joystickDirection;
    private Vector3 currentDirection;

    [Header("Fist")]
    private float fistSpeed = 10f;
    private Vector2 Direction = Vector2.zero;
    private Rigidbody2D rb;
    
    [SerializeField] private Transform originalFistPos; //Where the fist is on the arm
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        arm.SetActive(false);
        rb = fist.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isReturnToOrigin);  
        canRotate = onHold && !shooting; 

        if (canRotate)
            Rotate();


        Vector2 _direction = originalFistPos.position - fist.transform.position;
        float dist = Mathf.Sqrt(_direction.x * _direction.x + _direction.y * _direction.y);
        if(playerController.ReturnGrappleToInitialPosition)
        {
            GrappleReturned();
        }
        if(playerController.IsHunging)
        {
            RotateArmWhenHunging();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }


        if (playerController.ReturnGrappleToInitialPosition)
            RotateArmWhenGrappleReturn();


        else if (returning || dist > 10) /* limit fist distance, prevent it to go into infinity */
            ReturnFist();

        if (!playerController.IsHunging)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void FixedUpdate()
    {
        if(!playerController.IsHunging)
            FistMovement();
    }
    /* Activate the arm so he can be rotated */
    public void ActivateArm()
    {
        onHold = true;
        arm.SetActive(true);
    }
    /* Rotate arm toward the mouse's cursor */
    void Rotate()
    {
        fist.transform.localPosition = Vector3.zero;

        currentRotation = transform.rotation;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        cursorDirection = (cam.ScreenToWorldPoint(mousePos) - transform.position).normalized;

        if (PlayerController.instance.Controller.currentControlScheme == "K&M")
        {
            currentDirection = cursorDirection;
        }
        else if (PlayerController.instance.Controller.currentControlScheme == "GamePad")
        {
            currentDirection = joystickDirection;
        }

        /* Flip the character sprite depending of the mouse's position 
           The character should be able */
        if (currentDirection.x < 0)
            flipped = true;
        else 
            flipped = false;

        /* Clamp the rotation of the player depending of which side he's facing */
        if (flipped)
        {
            currentDirection.x *= -1;
            currentDirection.y *= -1;
        }

        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -90, 70);

        if (flipped)
        {
            angle += 180f;
        }

        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        //Debug.Log(angle);
    }

    /* Calculs to get where the fist will go */
    public void ShootFist()
    {
        if (!shooting)
        {
            canRotate = false;
            shooting = true;
            onHold = false;
            isReturnToOrigin = false;

            Direction = fist.transform.position - transform.position;
            
        }
    }
    
    /* Calculs to make the fist go back to his original point */
    public void ReturnFist()
    {
        returning = true;
        Direction = originalFistPos.position - fist.transform.position;
    }

    /* Actual movement of the fist */
    void FistMovement()
    {
        if (!isReturnToOrigin)
            rb.velocity = Direction.normalized * fistSpeed;
        else
            fist.transform.position = transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, rotationSpeed * Time.deltaTime); //Smooth the rotation

        FistReturned();
    }

    void FistReturned()
    {
        float dist = Vector2.Distance(originalFistPos.position, fist.transform.position); //Distance between the original position of the fist and the current position of it

        /* While the fist is returning we check the distance to know when we put the fist back to it's original place */
        if (returning && dist < 0.2f)
        {
            currentRotation = originalRotation;
            
            playerController.ReturnGrappleToInitialPosition = false;

            shooting = false;
            returning = false;
            canRotate = false;
            isReturnToOrigin = true;
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.z - originalRotation.eulerAngles.z)<1f && !canRotate && !shooting)
        {
            isReturnToOrigin = false;
            arm.SetActive(false);
        }
    }

    private void GrappleReturned()
    {
        playerController.IsHunging = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        ReturnFist();
        FistReturned();
    }


    void RotateArmWhenHunging()
    {
        Vector3 HangPointPosition = fist.GetComponent<Fist>().positionOfCollision;
        Quaternion rotation = Quaternion.LookRotation
            (HangPointPosition - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        fist.transform.position = HangPointPosition;
    }

    void RotateArmWhenGrappleReturn()
    {
        Vector3 FistPos = new Vector3(fist.transform.position.x, fist.transform.position.y, fist.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation
            (fist.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        fist.transform.position = FistPos;
    }

    public void SetDirection(Vector2 direction)
    {
        joystickDirection = direction;
    }
}
