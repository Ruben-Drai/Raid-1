using UnityEngine;

public class Fist : MonoBehaviour
{
    // Start is called before the first frame update
    private ArmController armController;
    void Start()
    {
        armController = GetComponentInParent<ArmController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && armController.shooting && !collision.isTrigger && collision.GetComponent<PlayerController>() == null)
        {
            armController.returning = true;
        }
    }
}
