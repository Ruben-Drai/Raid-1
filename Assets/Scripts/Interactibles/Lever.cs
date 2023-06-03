using System;
using UnityEngine;

public class Lever : Interactible
{
    [SerializeField] private bool move = false;

    [SerializeField] private bool moveOnce = false;
    [SerializeField] private bool moveDoOnce = false;

    [SerializeField] private bool appearance = false;
    [SerializeField] private Color colorShow = Color.white;
    [SerializeField] private Color colorHide = new Color(0.5566038f, 0.5566038f, 0.5566038f, 1);

    [SerializeField] private GameObject[] platforms;


    private void Update()
    {
        if (IsActivated)
        {
            for (int i =  0; i < platforms.Length; i++)
            {
                if (move)
                {
                    platforms[i].GetComponent<Platform>().IsActivated = true;
                }
                else if (moveOnce)
                {
                    Platform currentPlatform = platforms[i].GetComponent<Platform>();

                    if (moveDoOnce)
                    {
                        if (i == platforms.Length - 1)
                        {
                            moveDoOnce = false;
                        }

                        //moveDoOnce = i == platforms.Length - 1 ? false : true;
                        currentPlatform.IsActivated = true;
                            Debug.Log("yes");
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
            for (int i = 0; i < platforms.Length; i++)
            {
                if (move)
                {
                    platforms[i].GetComponent<Platform>().IsActivated = false;
                }
            }
        }
    }
    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"]) // use lever part.2
        {
            IsActivated = IsActivated ? false : true;
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);

            if (appearance)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    Color colorSprite = platforms[i].GetComponent<SpriteRenderer>().color;
                    colorSprite = colorSprite == colorShow ? colorHide : colorShow;
                    platforms[i].GetComponent<SpriteRenderer>().color = colorSprite;

                    bool collider = platforms[i].GetComponent<Collider2D>().enabled;
                    collider = collider ? false : true;
                    platforms[i].GetComponent<Collider2D>().enabled = collider;
                }
            }
            else if (moveOnce)
            {
                moveDoOnce = IsActivated ? true : false;
            }
        }
    }

}
