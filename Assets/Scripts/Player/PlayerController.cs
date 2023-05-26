using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float JumpForce = 10f,
                                  MovementSpeed = 10f;
    private bool Grounded;
    private Vector2 movement;
    
    [NonSerialized] public Interactible AvailableInteraction;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public PlayerInput Controller;
    [NonSerialized] public bool IsPushingBox = false;
    public Dictionary<string, bool> UnlockedUpgrades;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UnlockedUpgrades = new Dictionary<string, bool>()
        {
            {"Leg",false},
            {"Arm",false},
            {"ArmGun", false},
            {"DoubleJump",false}
        };
        Controller = GetComponent<PlayerInput>();  
    }
    // Update is called once per frame
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (Grounded && !IsPushingBox && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            Grounded = false;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        //TODO: make it so that it starts following the player from the moment he interacts with it
        if(context.performed && AvailableInteraction!=null)
        {
            AvailableInteraction.Interact();
        }
        if(AvailableInteraction != null && context.canceled && AvailableInteraction.GetComponent<BigBox>() !=null)
        {
            AvailableInteraction.Interact();
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed)
            pauseMenu.Escape();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("pressed fire button");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>()!=null)
        {
            AvailableInteraction = collision.GetComponent<Interactible>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Interactible>() != null 
            && AvailableInteraction == collision.GetComponent<Interactible>()
            && Vector2.Distance(transform.position, collision.transform.position)>0.5f)
        {
            AvailableInteraction = null;
        }
    }
    public void SetGrounded(bool state)
    {
        Grounded = state;
    }
    private void FixedUpdate()
    {
        if(rb!=null)
            rb.velocity = new(movement.x*MovementSpeed*(Grounded?1:0.5f)*(IsPushingBox?0.5f:1),rb.velocity.y);
    }

    
}
