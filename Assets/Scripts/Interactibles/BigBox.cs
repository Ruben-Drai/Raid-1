using UnityEngine;
public class BigBox : Interactible
{
    [SerializeField]
    private float lockedMass = 100000, unlockedMass = 10;
    private Rigidbody2D rb;
    private bool IsBeingLifted = false;

    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"])
        {
            IsBeingLifted = !IsBeingLifted;
            rb.mass = IsBeingLifted? unlockedMass: lockedMass;
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
        } 
    }
}
