using UnityEngine;
public class BigBox : Interactible
{

    private Rigidbody2D rb;
    private bool IsBeingLifted = false;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"])
        {
            IsBeingLifted = !IsBeingLifted;

            if (IsBeingLifted)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            }
            PlayerController.instance.IsPushingBox = !PlayerController.instance.IsPushingBox;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsBeingLifted)
        {
            rb.velocity = PlayerController.instance.rb.velocity;
            if (PlayerController.instance.AvailableInteraction == null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                IsBeingLifted = false;
                PlayerController.instance.IsPushingBox = false;
            }
        } 
    }
}
