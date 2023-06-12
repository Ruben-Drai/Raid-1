using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishDialogue : MonoBehaviour
{
    public DialogueManager manager;

    void Update()
    {
        if (manager.finishedAnimationDown)
        {
            PlayerController.instance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Game");
            this.gameObject.SetActive(false);
        }
    }
}
