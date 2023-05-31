using UnityEngine;
public class BigBox : Interactible
{

    private Rigidbody2D rb;
    private bool IsBeingLifted = false;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"])
        {
            IsBeingLifted = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerController.instance.IsPushingBox = true;
        }
    }
    public void StopInteraction()
    {
        IsBeingLifted = false;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        PlayerController.instance.IsPushingBox = false;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (IsBeingLifted)
        {
            //effectively follows player, I would love for it to be better but I don't have an easier method,
            //so I'll have to stick with the impossiblity of pushing the box if the player can't move an inch
            rb.velocity = new(PlayerController.instance.rb.velocity.x, rb.velocity.y);

            //releases box if the player somehow loses the box from the trigger box around the player
            if (PlayerController.instance.AvailableInteraction == null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                IsBeingLifted = false;
                PlayerController.instance.IsPushingBox = false;
            }
        }

        //freezes the box on X and the rotation on Z if the player doesn't interact with the box anymore or if it's falling
        if (Mathf.Abs(rb.velocity.y) > 0.1f) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }
}
