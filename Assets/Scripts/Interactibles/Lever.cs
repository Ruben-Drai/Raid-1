using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class Lever : Interactible
{
    [SerializeField] private bool move = false;
    [SerializeField] private bool moveOnce = false;
    [SerializeField] private bool door = false;

    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;

    private bool moveDoOnce = false;

    [SerializeField] private bool appearance = false;
    [SerializeField] private Color colorShow = Color.white;
    [SerializeField] private Color colorHide = new Color(0.5566038f, 0.5566038f, 0.5566038f, 1);

    [SerializeField] private GameObject[] platforms;


    [SerializeField] private CinemachineVirtualCamera[] vms;

    private Coroutine cutscene;

    private void Update()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            Platform currentPlatform = platforms[i].GetComponent<Platform>();

            if (IsActivated)
            {
                if (moveOnce)
                {
                    if (moveDoOnce)
                    {
                        currentPlatform.currentWaypointIndex = 1;
                        currentPlatform.IsActivated = true;
                        moveDoOnce = !(i == platforms.Length - 1);
                    }
                }
                else if (move)
                {
                    currentPlatform.IsActivated = true;
                }
            }
            else
            {
                if (moveOnce)
                {
                    if (moveDoOnce)
                    {
                        currentPlatform.currentWaypointIndex = 0;
                        currentPlatform.IsActivated = true;
                        moveDoOnce = !(i == platforms.Length - 1);
                    }
                }
                else if (move)
                {
                    currentPlatform.IsActivated = false;
                }
            }
        }
    }
    public override void Interact()
    {
        if (PlayerController.UnlockedUpgrades["Strength"] /*&&(!moveOnce ||(moveOnce && !IsActivated))*/)
        {
            GetComponent<AudioSource>().volume = SoundManager.instance!=null? SoundManager.instance.volumeSoundSlider.value :1;
            GetComponent<AudioSource>().Play();

            IsActivated = !IsActivated;
            moveDoOnce = true;

            transform.GetChild(0).gameObject.SetActive(!IsActivated);
            transform.GetChild(1).gameObject.SetActive(IsActivated);
            if(LaunchesCutscene) cutscene ??= StartCoroutine(LaunchCutscene());

            /* Move in and out platforms */
            if (appearance)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    Color colorSprite = platforms[i].GetComponent<SpriteRenderer>().color;
                    colorSprite = colorSprite == colorShow ? colorHide : colorShow;
                    platforms[i].GetComponent<SpriteRenderer>().color = colorSprite;

                    platforms[i].GetComponent<Collider2D>().enabled = !platforms[i].GetComponent<Collider2D>().enabled;
                }
            }
            else if (door)
            {
                moveDoOnce = true;
            }

            /* Open laser doors */
            if (door)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].transform.GetChild(0).gameObject.SetActive(!IsActivated);
                    platforms[i].transform.GetChild(1).gameObject.SetActive(IsActivated);
                }
            }
        }
    }
    public IEnumerator LaunchCutscene()
    {
        foreach(var pos in vms)
        {
            pos.Priority = 11;

            yield return new WaitForSeconds(CutsceneFreezeDuration);
            pos.Priority = 9;
        }
        yield return null;
    }
    
}