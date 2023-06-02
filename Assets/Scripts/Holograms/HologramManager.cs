using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HologramManager : MonoBehaviour
{
    public Animator holoAnimation;
    public AudioSource holoSound;

    public List<GameObject> holoBox;

    private bool isHologramActive = false;

    private void Start()
    {
    }

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
            holoAnimation.SetBool("IsActive", false);
            holoSound.Stop();

            isHologramActive = false;
        }
    }
}