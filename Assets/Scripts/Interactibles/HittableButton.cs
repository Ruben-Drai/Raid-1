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
            /*transform.GetChild(0).gameObject.SetActive(false);
            DoorClosed = false;*/

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
                    //platforms[i].GetComponent<Platform>().currentWaypointIndex = 0;
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
            IsActivated = true;
            moveDoOnce = true;
            Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player"))
        {
            IsActivated = false;
            Interact();
        }
    }
}
