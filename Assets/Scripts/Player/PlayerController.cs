using System;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float JumpForce = 17f, MovementSpeed = 10f, JumpCooldown = 0.1f, CoyoteTime = 0.2f, RopeShrinkSpeed=3f;
    [SerializeField] private GrapplingGun hook;
    [SerializeField] private BoxCollider2D StandingColl;
    [SerializeField] private BoxCollider2D SneakingColl;
    [SerializeField] private CapsuleCollider2D InteractionTrigger;

    private bool _canJump = true;
    private bool IsChangingLen = false;
    private bool IsSneaking = false;
    private float TimeFromLastJump = 0f;
    private float CoyoteTimer = 0f;
    private float lenModifier = 0f;
    private Vector2 movement;
    private GroundCheck feet;
    private SpringJoint2D joint;

    [NonSerialized] public Interactible AvailableInteraction;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public PlayerInput Controller;
    [NonSerialized] public Vector2 SlopeAdjustment;
    [NonSerialized] public bool IsPushingBox = false;
    [NonSerialized] public bool IsMoving = false;

    [NonSerialized] public bool IsInJump = true;
    [NonSerialized] public bool CanDoubleJump = true;

    public bool CanJump
    {
        get
        {
            return _canJump;
        }
        set
        {
            if ((!IsInJump && value == true) || (IsInJump && CoyoteTimer > CoyoteTime && value == false))
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
        joint = GetComponent<SpringJoint2D>();
        feet = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        UnlockedUpgrades = new Dictionary<string, bool>()
        {
            {"Jump",false},
            {"Sprint&Sneak",false},
            {"Strength",false},
            {"ArmGun", true},
            {"DoubleJump",true},
            {"Hook",true}
        };
        Controller = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        TimeFromLastJump += Time.deltaTime;
        CoyoteTimer += Time.deltaTime;
        if (hook.HasShot && IsChangingLen)
        {
            joint.distance += lenModifier*RopeShrinkSpeed * Time.deltaTime;
        }
        if (IsSneaking)
        {
            SneakingColl.enabled = true;
            InteractionTrigger.enabled = false;
            StandingColl.enabled = false;
        }
        else
        {
            if (!Physics2D.Raycast(transform.position, Vector2.up, 0.5f))
            {
                SneakingColl.enabled = false;
                InteractionTrigger.enabled = true;
                StandingColl.enabled = true;
            }
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        IsMoving = true;
    }
    //gamepad and mobile only
    public void Aim(InputAction.CallbackContext context)
    {
        hook.Direction = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (TimeFromLastJump > JumpCooldown
            && (_canJump || (CanDoubleJump && UnlockedUpgrades["DoubleJump"]))
            && !IsPushingBox
            && context.performed)
        {
            if (hook.HasShot)
                hook.ReturnHook();

            TimeFromLastJump = 0f;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            IsMoving = true;
            IsInJump = true;
            //just in case player spams button, since groundcheck is only done once every 0.1s
            if (_canJump)
                _canJump = false;
            else if (CanDoubleJump && UnlockedUpgrades["DoubleJump"])
                CanDoubleJump = false;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        //if the player is on the ground currently and not on an interactible such as a box so that you can't push a box to the right from the top of it
        if (!IsInJump && feet.groundState != GroundState.Interactibles)
        {
            if (AvailableInteraction != null && context.performed)
            {
                AvailableInteraction.Interact();
            }
            //if the interactible is a box, stop interacting with the box
            if (AvailableInteraction != null && context.canceled && AvailableInteraction.GetComponent<BigBox>() != null)
            {
                AvailableInteraction.GetComponent<BigBox>().StopInteraction();
            }
        }
    }
    public void Sneak(InputAction.CallbackContext context)
    {
        IsSneaking = !IsSneaking;
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
            pauseMenu.Escape();
    }
    public void ChangeHookLength(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsChangingLen = true;
            lenModifier = context.ReadValue<Vector2>().normalized.y;

        }
        else if (context.canceled) 
        {
            IsChangingLen = false;
            lenModifier = 0f;

        }



    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (!IsSneaking && UnlockedUpgrades["ArmGun"])
        {
            if (context.performed)
            {
                if (!hook.HasShot)
                {
                    hook.ActivateHook();
                }

            }
            else if (context.canceled)
            {
                if (hook.HasShot)
                    hook.ReturnHook();
                else
                    hook.FireHook();
            }
        }
    }
    //gets interactible from trigger box around the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null)
        {
            AvailableInteraction = collision.GetComponent<Interactible>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null
            && AvailableInteraction == collision.GetComponent<Interactible>()
            && Vector2.Distance(transform.position, collision.transform.position) > 0.5f)
        {
            AvailableInteraction = null;
        }
    }

    //Player movement
    private void FixedUpdate()
    {
        //state stuff
        if (movement.x == 0 && Mathf.Abs(rb.velocity.y) < 0.001)
            IsMoving = false;
        if (Mathf.Abs(rb.velocity.y) > 1f)
            IsInJump = true;

        //speed stuff
        float pushingBoxSlow = IsPushingBox ? 0.5f : 1;
        float airSpeedSlow = _canJump && CanDoubleJump ? 1 : 0.75f;
        float speed = pushingBoxSlow * airSpeedSlow * MovementSpeed;

        //slope stuff
        bool slope = feet.groundState == GroundState.Slope && !IsInJump;
        float SlopeMovementY = movement.normalized.x * -SlopeAdjustment.y * speed;

        if (rb != null)
            if(!hook.HasShot)
                rb.velocity = new(movement.normalized.x * speed * -SlopeAdjustment.x, slope ? SlopeMovementY : rb.velocity.y);
            else
                rb.AddForce(new(movement.normalized.x*speed,0),ForceMode2D.Force);
    }

}
