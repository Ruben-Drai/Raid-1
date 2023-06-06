using UnityEngine;
public class BigBox : Interactible
{

    private Rigidbody2D rb;
    private bool IsBeingLifted = false;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Strength"])
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
            rb.velocity = new(PlayerController.instance.rb.velocity.x, rb.velocity.y);
            transform.Find("Highlight")?.gameObject.SetActive(false); // Deactivates highlighting when the box is moved.
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
