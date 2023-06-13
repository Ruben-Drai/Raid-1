using Cinemachine;
using System.Collections;
using UnityEngine;


public class HittableButton : Interactible
{
    /*can be touched once or more*/
    [SerializeField] private bool canExplode = false;
    private bool isExploded = false;

    /*different modes*/
    [SerializeField] private bool move = false;
    [SerializeField] private bool door = false;
    [SerializeField] private bool appearance = false;
    [SerializeField]private bool returnToStart = false;

    private bool moveDoOnce = false;

    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;

    /*color for mode appearance*/
    [SerializeField] private Color colorShow = Color.white;
    [SerializeField] private Color colorHide = new Color(0.5566038f, 0.5566038f, 0.5566038f, 1);

    [SerializeField] private GameObject[] platforms;
    [SerializeField] private CinemachineVirtualCamera[] vms;


    private Coroutine cutscene;

    private void Update()
    {
        if(!door)
        
        for (int i = 0; i < platforms.Length; i++)
        {
            Platform currentPlatform = platforms[i].GetComponent<Platform>();

            if (IsActivated)
            {
                if (currentPlatform.moveOnce)
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
            else if (!returnToStart)
            {
                if (currentPlatform.moveOnce)
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
            else
            {
                currentPlatform.currentWaypointIndex = 1;
                currentPlatform.returnToStart = true;
            }
        }
    }

    public override void Interact()
    {
        IsActivated = !IsActivated;
        moveDoOnce = true;

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        if (LaunchesCutscene) cutscene ??= StartCoroutine(LaunchCutscene());


        if (door)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].transform.GetChild(0).gameObject.SetActive(!IsActivated);
                platforms[i].transform.GetChild(1).gameObject.SetActive(IsActivated);
            }
        }

        /* Activate or deactivate a platform's collider and show it by changing its color */
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate") 
            || collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("PlayerFist"))
        {
            if((canExplode && !isExploded) || !canExplode)
            {
                Interact();
                isExploded = collision.gameObject.CompareTag("PlayerFist") && canExplode;
                GetComponent<AudioSource>().volume = SoundManager.instance != null ? SoundManager.instance.volumeSoundSlider.value : 1;
                GetComponent<AudioSource>().Play();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate")
            || collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("PlayerFist"))
        {
            
            if(!canExplode && !collision.CompareTag("PlayerFist"))
            {
                Interact(); 
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else if(!canExplode)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
            else if(canExplode && collision.CompareTag("Player") && !isExploded)
            {
                Interact();
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
            
        }
       
    }

    public IEnumerator LaunchCutscene()
    {
        foreach (var pos in vms)
        {
            pos.Priority = 11;

            yield return new WaitForSeconds(CutsceneFreezeDuration);
            pos.Priority = 9;
        }
        yield return null;
    }
}