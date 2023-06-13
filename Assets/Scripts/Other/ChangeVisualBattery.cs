using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVisualBattery : MonoBehaviour
{
    public List<Sprite> sprites;
    public Image battery;
    public TextMeshProUGUI text;
    public Animator batteryAnimator;

    void Update()
    {
        if(GameUI.charge <= 100 && GameUI.charge > 84)
        {
            //BLUE
            battery.sprite = sprites[0];
            text.color = new Color32(0, 234, 255, 175);
        }
        else if (GameUI.charge <= 84 && GameUI.charge > 68)
        {
            //TEAL
            battery.sprite = sprites[1];
            text.color = new Color32(0, 255, 199, 175);
        }
        else if (GameUI.charge <= 68 && GameUI.charge > 52)
        {
            //GREEN
            battery.sprite = sprites[2];
            text.color = new Color32(0, 255, 93, 175);
        }
        else if (GameUI.charge <= 52 && GameUI.charge > 36)
        {
            //YELLOW
            battery.sprite = sprites[3];
            text.color = new Color32(254, 255, 0, 175);
        }
        else if (GameUI.charge <= 36 && GameUI.charge > 20)
        {
            //ORANGE
            battery.sprite = sprites[4];
            text.color = new Color32(255, 160, 0, 175);
        }
        else if (GameUI.charge <= 20 &&  GameUI.charge > 6)
        {
            //RED
            battery.sprite = sprites[5];
            text.color = new Color32(255, 21, 0, 175);
            batteryAnimator.SetBool("LowBattery", true);
        }
        else if (GameUI.charge <= 6 && GameUI.charge > 0)
        {
            //RED
            battery.sprite = sprites[6];
            text.color = new Color32(255, 21, 0, 175);
            batteryAnimator.SetBool("CriticalBattery", true);
        }
    }
}
