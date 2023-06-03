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
    [SerializeField] private int nbDo = 0;


    private void Update()
    {
        if (IsActivated)
        {
            for (int i =  0; i < platforms.Length; i++)
            {
                Platform currentPlatform = platforms[i].GetComponent<Platform>();

                if (move)
                {
                    currentPlatform.IsActivated = true;
                }
                else if (moveOnce)
                {
                    /*if (moveDoOnce)
                    {
                        moveDoOnce = i == platforms.Length - 1 ? false : true;
                        currentPlatform.IsActivated = true;
                    }

                    if (currentPlatform.isNear)
                    {
                        currentPlatform.isNear = false;
                        currentPlatform.IsActivated = false;
                    }*/
                }
            }
        }
        else
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                Platform currentPlatform = platforms[i].GetComponent<Platform>();

                if (move)
                {
                    currentPlatform.IsActivated = false;
                }
                /*else if (moveOnce)
                {
                    if (currentPlatform.isNear)
                    {
                        currentPlatform.isNear = false;
                        currentPlatform.IsActivated = false;
                    }
                }*/
            }
        }

        if (moveOnce)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                Platform currentPlatform = platforms[i].GetComponent<Platform>();

                if (moveDoOnce)
                {
                    moveDoOnce = i == platforms.Length - 1 ? false : true;
                    currentPlatform.IsActivated = true;
                }

                if (currentPlatform.isNear)
                {
                    currentPlatform.isNear = false;
                    currentPlatform.IsActivated = false;

                    nbDo = i == platforms.Length - 1 ? nbDo - 1 : nbDo;
                    moveDoOnce = nbDo <= 0 ? false : true;
                }
            }
        }
    }
    public override void Interact()
    {
        if (PlayerController.instance.UnlockedUpgrades["Arm"]) // use lever part.2
        {
            IsActivated = IsActivated ? false : true;
            nbDo += IsActivated ? 1 : 0;

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
                moveDoOnce = IsActivated;
            }
        }
    }

}
