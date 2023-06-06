using Cinemachine;
using System.Collections;
using UnityEngine;

public class HittableButton : Interactible
{
    [SerializeField] private bool move = false;
    [SerializeField] private bool LaunchesCutscene = false;
    [SerializeField] private bool canExplode = false;
    [SerializeField] private float CutsceneFreezeDuration = 2f;

    private bool moveDoOnce = false;
    private bool isExploded = false;

    [SerializeField] private GameObject[] platforms;


    private Coroutine cutscene;

    private void Update()
    {
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
        if ((collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player")) && !isExploded)
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