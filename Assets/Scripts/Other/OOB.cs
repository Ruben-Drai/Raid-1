using UnityEngine;

public class OOB : MonoBehaviour
{
    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = checkpoint.position;
        }
    }
}
