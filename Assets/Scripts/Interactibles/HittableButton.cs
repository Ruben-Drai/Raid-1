using Cinemachine;
using System.Collections;
using UnityEngine;
using static Cinemachine.CinemachinePathBase;

public class HittableButton : Interactible
{
    private bool isExploded = false;
    [SerializeField] private bool canExplode = false;
    [SerializeField] private bool move = false;
    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;
    [SerializeField] private bool door = false;

    private bool moveDoOnce = false;

    [SerializeField] private bool appearance = false;
    [SerializeField] private Color colorShow = Color.white;
    [SerializeField] private Color colorHide = new Color(0.5566038f, 0.5566038f, 0.5566038f, 1);

    [SerializeField] private GameObject[] platforms;


    private Coroutine cutscene;

    private void Update()
    {
        if(!door)
        
        for (int i = 0; i < platforms.Length; i++)
        {
            Platform currentPlatform = platforms[i].GetComponent<Platform>();


            if (IsActivated)
            {

                if (move)
                {
                    currentPlatform.IsActivated = true;
                }
                else if (currentPlatform.moveOnce)
                {
                    if (moveDoOnce)
                    {
                        currentPlatform.currentWaypointIndex = 1;
                        currentPlatform.IsActivated = true;
                        moveDoOnce = !(i == platforms.Length - 1);
                    }


                }
            }
            else
            {
                if (move)
                {
                    currentPlatform.IsActivated = false;
                }
                else if (currentPlatform.moveOnce)
                {
                    if (moveDoOnce)
                    {
                        currentPlatform.currentWaypointIndex = 0;
                        currentPlatform.IsActivated = true;
                        moveDoOnce = !(i == platforms.Length - 1);
                    }
                }
            }
        }
    }

    public override void Interact()
    {
        IsActivated = !IsActivated;
        moveDoOnce = true;

        transform.GetChild(0).gameObject.SetActive(!IsActivated);
        transform.GetChild(1).gameObject.SetActive(IsActivated);
        if (LaunchesCutscene && isExploded && canExplode) cutscene ??= StartCoroutine(LaunchCutscene());


        if (door)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].transform.GetChild(0).gameObject.SetActive(!IsActivated);
                platforms[i].transform.GetChild(1).gameObject.SetActive(IsActivated);

                platforms[i].GetComponent<Collider2D>().enabled = !platforms[i].GetComponent<Collider2D>().enabled;
            }
        }

        /* Activate or deactivate a platform's collider and show it by changing its color */
        if (appearance && IsActivated)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                Color colorSprite = platforms[i].GetComponent<SpriteRenderer>().color;
                colorSprite = colorSprite == colorShow ? colorHide : colorShow;
                platforms[i].GetComponent<SpriteRenderer>().color = colorSprite;

                platforms[i].GetComponent<Collider2D>().enabled = !platforms[i].GetComponent<Collider2D>().enabled;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player")) && !isExploded)
            || (collision.gameObject.CompareTag("PlayerFist") && !IsActivated))
        {
            Interact();
            isExploded = collision.gameObject.CompareTag("PlayerFist") && canExplode;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player")) || (collision.gameObject.CompareTag("PlayerFist") && IsActivated) && !isExploded)
        {
            Interact();
        }
       
    }

    public IEnumerator LaunchCutscene()
    {
        foreach (var platform in platforms)
        {
            FindFirstObjectByType<CinemachineVirtualCamera>().Follow = platform.transform;

            yield return new WaitForSeconds(CutsceneFreezeDuration);
        }
        yield return null;
        FindFirstObjectByType<CinemachineVirtualCamera>().Follow = PlayerController.instance.transform;
    }
}