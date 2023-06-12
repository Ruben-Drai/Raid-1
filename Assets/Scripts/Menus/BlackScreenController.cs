using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlackScreenController : MonoBehaviour
{
    public GameObject player;
    public GameObject Narator;
    public GameObject robot;
    public DialogueManager dialogueManager;
    public CanvasGroup blackScreen;

    [SerializeField] private int space;

    private bool fadeOut = false;

    void Start()
    {
        var v = FindObjectOfType<DialogueTrigger>();
        v.StartDialogue();
        PlayerController.instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Dialogue");
    }

    void Update()
    {
        if(space >= 1 && dialogueManager.finishedAnimationDown)
        {
            fadeOut = true;
        }

        if(fadeOut) 
        { 
            if(blackScreen.alpha >= 0) 
            {
                blackScreen.alpha -= Time.deltaTime;
                if(blackScreen.alpha == 1) 
                {
                    fadeOut = false;   
                }
            }
        }

        if(blackScreen.alpha == 0)
        {
            this.gameObject.SetActive(false);
            PlayerController.instance.Controller.SwitchCurrentActionMap("Game");
            robot.SetActive(true);
            Narator.SetActive(false);
        }
    }

    public void SpaceCounting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(dialogueManager.finishedAnimationUp)
                space++;      
        }
    }
}
