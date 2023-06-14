using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{

    public DialogueManager Losemanager;
    public DialogueManager Winmanager;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayLogo.HasWon && Losemanager.finishedAnimationDown
            || PlayLogo.HasWon && Winmanager.finishedAnimationDown) 
        {
            animator.SetBool("PlayingCredit", true);
        }
    }
}
