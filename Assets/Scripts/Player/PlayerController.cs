using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float JumpForce = 17f, MovementSpeed = 10f, JumpCooldown = 0.1f, CoyoteTime=0.2f;

    private bool _canJump = true;
    private float TimeFromLastJump = 0f;
    private float CoyoteTimer = 0f;
    private Vector2 movement;
    private GroundCheck feet;
    private ArmController arm;


    [NonSerialized] public Interactible AvailableInteraction;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public PlayerInput Controller;
    [NonSerialized] public Vector2 SlopeAdjustment;
    [NonSerialized] public bool IsPushingBox = false;
    [NonSerialized] public bool IsMoving = false;
    [NonSerialized] public bool IsInJump = true;
    [NonSerialized] public bool CanDoubleJump = true;
    [NonSerialized] public InputActionMap actionMap;

    public bool CanJump 
    { 
        get 
        { 
            return _canJump; 
        }
        set
        {
            if ((!IsInJump && value == true) || (IsInJump && CoyoteTimer > CoyoteTime && value==false))
            {
                _canJump = value;
                CoyoteTimer = 0f;
            }
        }
    }
    public Dictionary<string, bool> UnlockedUpgrades;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        //Debug.Log(instance.actionMap.FindAction("Interact").bindings[0].effectivePath);  // test ~~ take error

        arm = GetComponentInChildren<ArmController>();
        feet = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        actionMap = GetComponent<PlayerInput>().actions.actionMaps[0];
        UnlockedUpgrades = new Dictionary<string, bool>()
        {
            {"Leg",false},
            {"Arm",false},
            {"ArmGun", false},
            {"DoubleJump",true}
        };
        Controller = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        TimeFromLastJump += Time.deltaTime;
        CoyoteTimer += Time.deltaTime;
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        IsMoving = true;
    }
    //gamepad and mobile only
    public void Aim(InputAction.CallbackContext context)
    {
        arm.SetDirection(context.ReadValue<Vector2>());
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (TimeFromLastJump > JumpCooldown && _canJump && !IsPushingBox && context.performed && !arm.LimitMovement)
        {
            TimeFromLastJump = 0f;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            IsMoving = true;
            IsInJump = true;
            //just in case player spams button, since groundcheck is only done once every 0.1s
            if (CanDoubleJump && UnlockedUpgrades["DoubleJump"])
                CanDoubleJump = false;

            else _canJump = false;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        //if the player is on the ground currently
        if (!IsInJump && feet.groundState != GroundState.Interactibles)
        {

            //TODO: make it so that it starts following the player from the moment he interacts with it
            if (AvailableInteraction != null && context.performed)
            {
                AvailableInteraction.Interact();
            }
            //if the interactible is a box, call interact when key is released, problem when key is released and box is redetected
            if (AvailableInteraction != null && context.canceled && AvailableInteraction.GetComponent<BigBox>() != null)
            {
                AvailableInteraction.GetComponent<BigBox>().StopInteraction();
                AvailableInteraction.transform.Find("Highlight").gameObject.SetActive(true);
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
            pauseMenu.Escape();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (rb.velocity.y == 0)
        {
            if (UnlockedUpgrades["ArmGun"])
            {
                if(context.performed)
                    arm.ActivateArm();
                else if(context.canceled)
                    arm.ShootFist();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null)
        {
            AvailableInteraction = collision.GetComponent<Interactible>();
            AvailableInteraction.transform.Find("Highlight").gameObject.SetActive(true); // Activates highlighting when the player is close by
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null
            && AvailableInteraction == collision.GetComponent<Interactible>()
            && Vector2.Distance(transform.position, collision.transform.position) > 0.5f)
        {
            AvailableInteraction.transform.Find("Highlight").gameObject.SetActive(false); // Deactivates highlighting when player moves away.
            AvailableInteraction = null;
        }
    }
    private void FixedUpdate()
    {
        if (movement.x == 0 && Mathf.Abs(rb.velocity.y) < 0.001) 
            IsMoving = false;
        if (Mathf.Abs(rb.velocity.y)>1f) 
            IsInJump = true;

        //speed stuff
        float pushingBoxSlow = IsPushingBox ? 0.5f : 1;
        float airSpeedSlow = _canJump && CanDoubleJump ? 1 : 0.5f;
        float Immobilize = arm.LimitMovement ? 0 : 1;
        float speed = pushingBoxSlow * airSpeedSlow * MovementSpeed;

        //slope stuff
        bool slope = feet.groundState == GroundState.Slope && !IsInJump;
        float SlopeMovementY = movement.x * -SlopeAdjustment.y * speed;

        if (rb != null)
            rb.velocity = new(movement.x * speed * Immobilize * -SlopeAdjustment.x, slope ? SlopeMovementY : rb.velocity.y);
    }


}
