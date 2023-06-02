using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public GameObject DialogueBox;
    public GameObject HologramBox;
    public Animator dialogueAnimation;

    public DialogueManager DialogueManager;

    public void StartDialogue()
    {
        DialogueBox.SetActive(true);
        FindObjectOfType<DialogueManager>().OpenDialogues(messages, actors);
        dialogueAnimation.SetBool("DialogueIsActive", true);
    }

    public void DialogueSetting()
    {
        DialogueManager.isActive = false;
        DialogueManager.messageText.text = "";
        FindObjectOfType<DialogueManager>().OpenDialoguesSetting(messages, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorID;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
    internal int actorID;
}