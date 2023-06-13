using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HologramManager : MonoBehaviour
{
    public Animator holoAnimation;
    public AudioSource holoSound;
    public HologramEnter trigger;

    public List<GameObject> buttons;
    public List<GameObject> messages;
    public List<GameObject> holoBox;

    public bool isHologramActive = false;

    public void Hologram()
    {
        if (!isHologramActive)
        {
            holoAnimation.SetBool("IsActive", true);
            holoSound.Play();

            for (int i = 0; i < holoBox.Count; i++)
            {
                holoBox[i].SetActive(true);
            }
            isHologramActive = true;
        }
    }

    public void Close(InputAction.CallbackContext context)
    {
        if (context.performed && isHologramActive)
        {
            for (int i = 0; i < holoBox.Count; i++)
            {
                holoBox[i].SetActive(false);
            }
            trigger.Interact();
            holoAnimation.SetBool("IsActive", false);
            holoSound.Stop();
            TypingEffect.canSkip = false;
            //Value ? out of my ass...
            TypingEffect.delayBeforeStart = 4f;
            isHologramActive = false;
            PlayerController.instance.Controller.SwitchCurrentActionMap("Game");
            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].SetActive(false);
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }

        }
    }

    public void ButtonClose()
    {
        if (isHologramActive)
        {
            for (int i = 0; i < holoBox.Count; i++)
            {
                holoBox[i].SetActive(false);
            }
            trigger.Interact();
            TypingEffect.canSkip = false;
            holoAnimation.SetBool("IsActive", false);
            holoSound.Stop();
            //Value ? out of my ass...
            TypingEffect.delayBeforeStart = 4f;
            isHologramActive = false;
            PlayerController.instance.Controller.SwitchCurrentActionMap("Game");
            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].SetActive(false);
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }
}