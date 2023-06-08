using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramChangeMessage : MonoBehaviour
{
    public List<GameObject> messages;

    public GameObject rightButton;
    public GameObject leftButton;

    public TypingEffect typingEffect;

    [SerializeField] private int currentIndex;

    public void Right()
    {
        currentIndex++;
        if (currentIndex >= messages.Count)
            currentIndex = 0;

        ShowCurrentMessage();
    }

    public void Left()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = messages.Count - 1;

        ShowCurrentMessage();
    }

    private void ShowCurrentMessage()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            if (i == currentIndex)
                messages[i].SetActive(true);
            else
                messages[i].SetActive(false);
        }

        leftButton.SetActive(currentIndex > 0);
        rightButton.SetActive(currentIndex < messages.Count - 1);
        
        TypingEffect.delayBeforeStart = 0;
    }

    public void SetActiveRightButton()
    {
        rightButton.SetActive(true);
        TypingEffect.canSkip = true;
    }
}
