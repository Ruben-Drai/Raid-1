using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLogo : MonoBehaviour
{
    public Animator logo;
    public GameObject imglogo;

    public void PlayAnimation()
    {
        imglogo.SetActive(true);
        logo.SetBool("isPlaying", true);
    }

}
