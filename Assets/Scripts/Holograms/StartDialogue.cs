using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartDialogue : MonoBehaviour
{
    public DialogueManager manager;
    void Start()
    {
        FindObjectOfType<DialogueTrigger>().StartDialogue();
        PlayerController.instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Dialogue");
    }

    void Update()
    {
        if(manager.finishedAnimationDown) 
        {
            PlayerController.instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Game");
            this.gameObject.SetActive(false);
        }       
    }
}
