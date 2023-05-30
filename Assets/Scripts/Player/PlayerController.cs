using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float JumpForce = 17f, MovementSpeed = 10f, JumpCooldown = 0.1f;

    private bool _canJump = true;
    private float TimeFromLastJump = 0f;
    private Vector2 movement;
    private GroundCheck feet;

    [NonSerialized] public Interactible AvailableInteraction;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public PlayerInput Controller;
    [NonSerialized] public Vector2 SlopeAdjustment;
    [NonSerialized] public bool IsPushingBox = false;
    [NonSerialized] public bool IsMoving = false;
    [NonSerialized] public bool IsInJump = true;
    [NonSerialized] public bool CanDoubleJump = true;

    public bool CanJump { get { return _canJump; } set { if (!IsInJump) _canJump = value; } }
    public Dictionary<string, bool> UnlockedUpgrades;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        feet = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        UnlockedUpgrades = new Dictionary<string, bool>()
        {
            {"Leg",false},
            {"Arm",true},
            {"ArmGun", false},
            {"DoubleJump",false}
        };
        Controller = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        TimeFromLastJump += Time.deltaTime;
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        IsMoving = true;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (TimeFromLastJump > JumpCooldown && _canJump && !IsPushingBox && context.performed)
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
    }
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
    private void FixedUpdate()
    {
        if (movement.x == 0 && Mathf.Abs(rb.velocity.y) < 0.001) IsMoving = false;
        if (rb.velocity.y < -0.4f) IsInJump = true;

        float pushingBoxSlow = IsPushingBox ? 0.5f : 1;
        float airSpeedSlow = _canJump && CanDoubleJump ? 1 : 0.5f;
        float speed = pushingBoxSlow * airSpeedSlow * MovementSpeed;
        if (rb != null) //Don't even ask about the formula, I destroyed my brain doing this
            rb.velocity = new(movement.x * speed * -SlopeAdjustment.x,
                 !IsInJump && _canJump && IsMoving ? movement.x * -SlopeAdjustment.y * speed : rb.velocity.y);

    }


}
