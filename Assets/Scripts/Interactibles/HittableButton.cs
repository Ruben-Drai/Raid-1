using UnityEngine;

public class HittableButton : Interactible
{
    private bool DoorClosed = true;

    [SerializeField] private bool moveOnce = false;
    private bool moveDoOnce = false;

    [SerializeField] private GameObject[] platforms;

    private void Update()
    {
        if (IsActivated)
        {
            if (moveOnce)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    Platform currentPlatform = platforms[i].GetComponent<Platform>();

                    if (moveDoOnce)
                    {
                        currentPlatform.IsActivated = true;
                        moveDoOnce = i == platforms.Length - 1 ? false : true;
                    }

                    if (currentPlatform.isNear)
                    {
                        currentPlatform.isNear = false;
                        currentPlatform.IsActivated = false;
                    }
                }
            }
        }
        else
        {
            if (moveOnce)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    Platform currentPlatform = platforms[i].GetComponent<Platform>();

                    currentPlatform.currentWaypointIndex = 1;
                    currentPlatform.MoveRoutine = currentPlatform.StartCoroutine(currentPlatform.IMoveRoutine());

                    if (currentPlatform.isNear)
                    {
                        currentPlatform.isNear = false;
                        currentPlatform.IsActivated = false;
                    }
                }
            }
        }
    }
    public override void Interact()
    {
        /*if (PlayerController.instance.UnlockedUpgrades["ArmGun"] && DoorClosed)
        {
            IsActivated = true;
            transform.localScale /= 2;
        }*/

        IsActivated = IsActivated ? false : true;
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("PlayerFist"))
        {
            Interact();
        }*/

        if (collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("PlayerFist") || collision.gameObject.CompareTag("Player"))
        {
            moveDoOnce = true;
            Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player"))
        {
            Interact();
        }
    }
}
