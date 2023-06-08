using UnityEngine;

public class OOB : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip Fall;

    [SerializeField] private Transform checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = checkpoint.position;
            GetComponent<AudioSource>().volume = SoundManager.instance == null ? 0.7f : SoundManager.instance.volumeSoundSlider.value;

            GetComponent<AudioSource>().PlayOneShot(Fall);
        }
    }
}
