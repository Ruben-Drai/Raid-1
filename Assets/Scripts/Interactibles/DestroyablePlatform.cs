using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    [SerializeField] private Sprite destroyedSprite;

    private bool isDestroyed;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDestroyed)
        {
            if (collision.gameObject.CompareTag("Crate"))
            {
                isDestroyed = true;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = destroyedSprite;
            }
        }
    }
}
