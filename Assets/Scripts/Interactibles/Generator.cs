using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : Interactible
{
    private Animator animator;
    private static int BossHP = 3;
    [SerializeField] private AudioClip explode;

    [SerializeField] private DestroyablePlatform platform;

    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    public GameObject dialogue;

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

            dialogue.SetActive(true);
            dialogueTrigger.StartDialogue();
        }
    }

    /* Get called when the exploding animation ends */
    public void DestroyObject()
    {
        if (platform != null)
        {
            platform.Interact();
        }
        if (BossHP <= 0 && dialogueManager.finishedAnimationDown) 
        {
            PlayLogo.HasWon = true;
            SceneManager.LoadScene("Credits");
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
