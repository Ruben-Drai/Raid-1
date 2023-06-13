using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayLogo : MonoBehaviour
{
    public Animator logo;
    public GameObject imglogo;
    public static bool HasWon;
    [SerializeField] private GameObject dialogueWin;
    [SerializeField] private GameObject dialogueLose;

    private void Start()
    {
        if (HasWon)
        {
            dialogueWin.SetActive(true);
        }
        else
        {
            dialogueLose.SetActive(true);
        }
    }
    public void PlayAnimation()
    {
        imglogo.SetActive(true);
        logo.SetBool("isPlaying", true);
    }

}
