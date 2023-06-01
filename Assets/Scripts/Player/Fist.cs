/*using UnityEngine;

public class Fist : MonoBehaviour
{
    // Start is called before the first frame update
    private ArmController armController;
    private PlayerController playerController;
    public Vector3 positionOfCollision;
    void Start()
    {
        armController = GetComponentInParent<ArmController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision!=null && collision.CompareTag("CanBeHung"))
        {
            if(!playerController.IsHanging)
            {
                positionOfCollision = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
                
            playerController.IsHanging = true;
        }
        else if(collision != null && armController.shooting && !collision.isTrigger && collision.GetComponent<PlayerController>() == null)
        {
            armController.returning = true;
        }
    }
}
*/