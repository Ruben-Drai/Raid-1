using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : Interactible
{
    private Animator animator;
    private static int BossHP = 3;
    [SerializeField] private AudioClip explode;

    [SerializeField] private DestroyablePlatform platform;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!IsActivated)
        {
            IsActivated = true;
            BossHP--;

            GetComponent<AudioSource>().PlayOneShot(explode);

            /* animation of exploson will play */
            animator.SetBool("Exploding", true);
        }
    }

    /* Get called when the exploding animation ends */
    public void DestroyObject()
    {
        if (platform != null)
        {
            platform.Interact();
        }
        if (BossHP <= 0) Debug.Log("Enemy felled");
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
