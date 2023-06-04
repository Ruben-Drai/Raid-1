using UnityEngine;

public class HittableButton : Interactible
{
    [SerializeField] private bool move = false;
    [SerializeField] private bool moveOnce = false;
    private bool moveDoOnce = true;

    [SerializeField] private GameObject[] platforms;

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
                else if (moveOnce)
                {
                    if (moveDoOnce)
                    {
                        moveDoOnce = i == platforms.Length - 1 ? false : true;
                        currentPlatform.IsActivated = true;
                    }

                    if (currentPlatform.isNear)
                    {
                        currentPlatform.isNear = false;
                        currentPlatform.IsActivated = false;
                    }
                }
            }
            else
            {
                if (move)
                {
                    currentPlatform.IsActivated = false;
                }
                else if (moveOnce)
                {
                    /*if (moveDoOnce)
                    {
                        currentPlatform.currentWaypointIndex = 1;
                        currentPlatform.MoveRoutine = currentPlatform.StartCoroutine(currentPlatform.IMoveRoutine());

                        moveDoOnce = i == platforms.Length - 1 ? false : true;
                    }*/

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
        IsActivated = !IsActivated;
        //moveDoOnce = true;
        transform.GetChild(0).gameObject.SetActive(!IsActivated);
        transform.GetChild(1).gameObject.SetActive(IsActivated);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Crate") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerFist"))
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
