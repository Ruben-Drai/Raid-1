using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVisualBattery : MonoBehaviour
{
    public List<Sprite> sprites;
    public Image battery;

    void Update()
    {
        if(GameUI.charge < 100 && GameUI.charge > 84)
        {
            battery.sprite = sprites[0];
        }
        else if (GameUI.charge < 84 && GameUI.charge > 68)
        {
            battery.sprite = sprites[1];
        }
        else if (GameUI.charge < 68 && GameUI.charge > 52)
        {
            battery.sprite = sprites[2];
        }
        else if (GameUI.charge < 52 && GameUI.charge > 36)
        {
            battery.sprite = sprites[3];
        }
        else if (GameUI.charge < 36 && GameUI.charge > 20)
        {
            battery.sprite = sprites[4];
        }
        else if (GameUI.charge < 20 &&  GameUI.charge > 6)
        {
            battery.sprite = sprites[5];
        }
        else if (GameUI.charge < 6 && GameUI.charge > 0)
        {
            battery.sprite = sprites[6];
        }
    }
}
