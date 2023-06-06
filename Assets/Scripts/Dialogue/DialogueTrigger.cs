using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public GameObject DialogueBox;
    public Animator dialogueAnimation;

    public DialogueManager DialogueManager;

    public void ResetPosition()
    {
        DialogueBox.transform.position = new Vector3(DialogueBox.transform.position.x, -500, DialogueBox.transform.position.z);
    }
    public void StartDialogue()
    {
        DialogueBox.SetActive(true);
        FindObjectOfType<DialogueManager>().OpenDialogues(messages, actors);
        dialogueAnimation.SetBool("DialogueIsActive", true);
    }

    public void DialogueSetting()
    {
        DialogueManager.messageText.text = "";
        DialogueManager.isActive = false;
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