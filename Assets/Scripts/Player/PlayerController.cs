using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private float JumpForce = 10f,
                                  MovementSpeed = 10f;
    private bool Grounded;
    private Rigidbody2D rb;
    private Vector2 movement;

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
        if (Grounded && !IsPushingBox)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            Grounded = false;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        //TODO: make it so that it starts following the player from the moment he interacts with it
        if(Interactible.CurrentInteractibleObject != null)
        {
            Interactible.CurrentInteractibleObject.Interact();
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        PauseMenu.SetActive(Time.timeScale==1?true:false);
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("pressed fire button");
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
