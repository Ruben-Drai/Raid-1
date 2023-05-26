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
            rb.constraints = IsBeingLifted ? RigidbodyConstraints2D.None:RigidbodyConstraints2D.FreezePositionX;
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
            rb.velocity = PlayerController.instance.GetComponent<Rigidbody2D>().velocity;
            if (PlayerController.instance.AvailableInteraction == null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                IsBeingLifted = false;
                PlayerController.instance.IsPushingBox = false;
            }
        } 
    }
}
