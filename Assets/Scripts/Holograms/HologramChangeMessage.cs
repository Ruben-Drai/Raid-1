using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramChangeMessage : MonoBehaviour
{
    public GameObject rightMessage;
    public GameObject leftMessage;

    public GameObject rightButton;
    public GameObject leftButton;

    public TypingEffect typingEffect;

    public void Right()
   {
        rightMessage.SetActive(true);
        leftMessage.SetActive(false);  
        
        leftButton.SetActive(true);
        rightButton.SetActive(false);

        TypingEffect.delayBeforeStart = 0;
   }
    
    public void Left()
    {
        leftMessage.SetActive(true);
        rightMessage.SetActive(false);

        leftButton.SetActive(false);
        rightButton.SetActive(true);
        TypingEffect.delayBeforeStart = 0;

    }

    public void SetActiveRightButton()
    {
        rightButton.SetActive(true);
        TypingEffect.canSkip = true;
    }
}
