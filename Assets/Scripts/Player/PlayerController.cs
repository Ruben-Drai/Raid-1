using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private float JumpForce = 17f, MovementSpeed = 10f, JumpCooldown = 0.1f, CoyoteTime = 0.2f, RopeShrinkSpeed=3f,walkSFXWaitTime =0.2f;
    [SerializeField] private BoxCollider2D StandingColl;
    [SerializeField] private BoxCollider2D SneakingColl;
    [SerializeField] private AudioClip Walk;
    [SerializeField] private AudioClip DoubleJump;
    [SerializeField] private CinemachineBrain CineBrain;

    public GrapplingGun hook;
    public LayerMask SneakIgnoreCheckLayers;

    private bool CantMove = false;
    private bool _canJump = true;
    private bool IsChangingLen = false;
    private bool IsSneaking = false;
    private float TimeFromLastJump = 0f;
    private float CoyoteTimer = 0f;
    private float lenModifier = 0f;
    private Vector2 movement;
    private GroundCheck feet;
    private SpringJoint2D joint;
    private Animator animator;



    [NonSerialized] public Interactible AvailableInteraction;
    [NonSerialized] public Rigidbody2D rb;
    [NonSerialized] public PlayerInput Controller;
    [NonSerialized] public InputActionMap actionMap;

    [NonSerialized] public Vector2 SlopeAdjustment;
    [NonSerialized] public Vector2 GamePadAimDirection;

    [NonSerialized] public bool IsPushingBox = false;
    [NonSerialized] public bool IsMoving = false;
    [NonSerialized] public bool IsInJump = true;
    [NonSerialized] public bool CanDoubleJump = true;
    [NonSerialized] public bool IsAtMyLeft = false;

    private bool IsDoubleJumping = false;

    private Coroutine WalkSFXRoutine;
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
    public static Dictionary<string, bool> UnlockedUpgrades = null;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        joint = GetComponent<SpringJoint2D>();
        feet = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        actionMap = GetComponent<PlayerInput>().actions.actionMaps[0];
        if(UnlockedUpgrades==null) 
            UnlockedUpgrades = new Dictionary<string, bool>()
        {
            {"Jump",true},
            {"Strength",false},
            {"ArmGun", false},
            {"DoubleJump&Sneak",false},
            {"Hook",false}
        };
        Controller = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        Animation();
        if (hook.HasShot)
        {
            _canJump = false;
            if (UnlockedUpgrades["DoubleJump&Sneak"])
                CanDoubleJump = true;
        }
        if (feet.groundState != GroundState.Air) IsDoubleJumping = false;
        TimeFromLastJump += Time.deltaTime;
        CoyoteTimer += Time.deltaTime;
        if (hook.HasShot && IsChangingLen)
        {
            joint.distance += lenModifier*RopeShrinkSpeed * Time.deltaTime;
        }
        if (IsSneaking)
        {
            SneakingColl.enabled = true;
            StandingColl.enabled = false;
        }
        else
        {
            if (CanUncrouch)
            {
                SneakingColl.enabled = false;
                StandingColl.enabled = true;
            }
        }
        if(IsPushingBox && IsInJump)
        {
            AvailableInteraction.Interact();
            AvailableInteraction = null;
        }
        if (CineBrain != null)
        {
            if (CineBrain.ActiveVirtualCamera.Priority == 11)
            {
                CantMove = true;
            }
            else CantMove = false;
        }
    }
    private bool CanUncrouch
    {
        get 
        {
            return !Physics2D.Raycast(transform.position, Vector2.up, 0.385f*transform.lossyScale.x, SneakIgnoreCheckLayers)
            && !Physics2D.Raycast(transform.position - new Vector3(0.18f * transform.lossyScale.x, 0, 0), Vector2.up, 0.385f * transform.lossyScale.x, SneakIgnoreCheckLayers)
            && !Physics2D.Raycast(transform.position + new Vector3(0.18f*transform.lossyScale.x,0,0), Vector2.up, 0.385f * transform.lossyScale.x, SneakIgnoreCheckLayers);
        }
        
    }
    private bool isCrouching
    {
        get
        {
            return IsSneaking ? true : !IsSneaking && !CanUncrouch;
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
        GamePadAimDirection = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!CantMove && 
            TimeFromLastJump > JumpCooldown
            && ((_canJump && UnlockedUpgrades["Jump"]) 
            || (CanDoubleJump && UnlockedUpgrades["DoubleJump&Sneak"]))
            && !IsPushingBox
            && context.performed
            && ((isCrouching && CanUncrouch) || !isCrouching))
        {
            if(hook.HasShot)
                hook.ReturnHook();


            IsSneaking = false;
            TimeFromLastJump = 0f;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            IsMoving = true;
            IsInJump = true;
            //just in case player spams button, since groundcheck is only done once every 0.1s
            if (_canJump && UnlockedUpgrades["Jump"])
            {
                _canJump = false;
                IsDoubleJumping = false;
                

            }
            else if (CanDoubleJump && UnlockedUpgrades["DoubleJump&Sneak"])
            {
                GetComponent<AudioSource>().volume = SoundManager.instance == null ? 1f : SoundManager.instance.volumeSoundSlider.value;
                GetComponent<AudioSource>().PlayOneShot(DoubleJump);
                CanDoubleJump = false;
                IsDoubleJumping = true;
            }
                
        }
               
    }
    
    public void Interact(InputAction.CallbackContext context)
    {
        //if the player is on the ground currently and not on an interactible such as a box so that you can't push a box to the right from the top of it
        if (!CantMove && !IsInJump && feet.groundState != GroundState.Interactibles && AvailableInteraction != null && context.performed)
        {
            if(AvailableInteraction.GetComponent<BigBox>()!=null && !IsSneaking)  
                AvailableInteraction.Interact();
            else if (AvailableInteraction.GetComponent <BigBox>()== null)
                AvailableInteraction.Interact();
        }
    }
    public void Sneak(InputAction.CallbackContext context)
    {
        if (!CantMove && UnlockedUpgrades["DoubleJump&Sneak"] && !IsInJump && !hook.HasShot)
        {
            hook.ReturnHook();
            if(context.performed)IsSneaking = true;
            else if (context.canceled) IsSneaking = false;
        }
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
            pauseMenu.Escape();
    }
    public void ChangeHookLength(InputAction.CallbackContext context)
    {
        if (!CantMove && context.performed)
        {
            var temp = -context.ReadValue<Vector2>().normalized.y;

            if (feet.groundState == GroundState.Air)
            {
                IsChangingLen = true;
                lenModifier = temp;
            }
            else
            {
                if (temp<0)
                {
                    IsChangingLen = true;
                    lenModifier = temp;
                }
            }

        }
        else if (!CantMove && context.canceled) 
        {
            IsChangingLen = false;
            lenModifier = 0f;

        }



    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (!CantMove && !IsPushingBox && !IsSneaking && UnlockedUpgrades["ArmGun"] && Time.timeScale==1)
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
                if (hook.HasShot && UnlockedUpgrades["Hook"])
                    hook.ReturnHook();
                else if (hook.gameObject.activeSelf && !hook.HasShot)
                {
                    hook.FireHook();
                    IsSneaking = false;
                }
            }
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
        float crouchModifier = isCrouching ? 0.5f : 1;
        //slope stuff
        bool slope = feet.groundState == GroundState.Slope && !IsInJump;
        float SlopeMovementY = movement.normalized.x * -SlopeAdjustment.y * speed;

        if (rb != null)
        {
            if (!CantMove)
            {
                if (!hook.HasShot || !UnlockedUpgrades["Hook"])
                    rb.velocity = new(movement.normalized.x * speed * -SlopeAdjustment.x * crouchModifier, slope ? SlopeMovementY : rb.velocity.y);
                else
                    rb.AddForce(new(movement.normalized.x * speed, 0), ForceMode2D.Force);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }

        if (IsMoving && !IsInJump && !GetComponent<AudioSource>().isPlaying)
        {

        }
        else if (!IsMoving && !IsInJump && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    private IEnumerator IWalkSFXRoutine()
    {
        while (WalkSFXRoutine != null)
        {
            GetComponent<AudioSource>().volume = SoundManager.instance == null ? 1f : SoundManager.instance.volumeSoundSlider.value;
            GetComponent<AudioSource>().PlayOneShot(Walk);
            yield return new WaitForSeconds(walkSFXWaitTime);
        } 
    }
    private void Animation()
    {
        animator.SetBool("IsGrappling", hook.HasShot);
        animator.SetBool("IsGrounded", feet.groundState != GroundState.Air);
        animator.SetBool("UnlockArm", UnlockedUpgrades["Strength"]);
        animator.SetBool("UnlockLegs", UnlockedUpgrades["Jump"]);
        animator.SetFloat("Velocity_Y", rb.velocity.y);
        animator.SetBool("DoubleJump", IsDoubleJumping);
        animator.SetBool("IsMoving", IsMoving);
        animator.SetBool("IsPushingBox", IsPushingBox);
        animator.SetBool("IsCrouch", isCrouching);
        if (IsPushingBox)
        {
            if (IsAtMyLeft)
                GetComponent<SpriteRenderer>().flipX = true;
            else
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            if (rb.velocity.x > 0.1f)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (rb.velocity.x < -0.1f)
                GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
