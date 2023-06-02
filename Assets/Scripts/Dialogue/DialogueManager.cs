using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Animations")]
    public Animator dialogueAnimation;
    public Animator arrowAnimation;

    [Header("DialogueBox")]
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    [Header("Name Color")]
    public Color robot = new Color(198f, 0f, 0f);
    public Color narator = new Color(255f, 128f, 0f);
    public Color system = new Color(0f, 204f, 0f);
    public Color ai = new Color(0f, 0f, 204f);

    public Slider speedText;
    private Coroutine displayLineCoroutine;
    private bool canSkip = false;
    private bool submitSkip;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    private Dictionary<int, Color> actorNameColors = new Dictionary<int, Color>();
    public DialogueTrigger dialogueTrigger;

    public void ChangeTextSpeed()
    {
        switch(speedText.value)
        {
            case 0:
                typingSpeed = 0.07f;
                dialogueTrigger.DialogueSetting();
                break;
            case 1:
                typingSpeed = 0.04f;
                dialogueTrigger.DialogueSetting();
                break;
            case 2:
                typingSpeed = 0.01f;
                dialogueTrigger.DialogueSetting();
                break;
            default:
                typingSpeed = 0.04f;
                dialogueTrigger.DialogueSetting();
                break;
        }
    }
    public void InitializeActorNameColors()
    {
        actorNameColors.Add(0, robot); 
        actorNameColors.Add(1, narator); 
        actorNameColors.Add(2, system); 
        actorNameColors.Add(3, ai); 
    }

    private void Start()
    {
        //InitializeActorNameColors();
    }

    public void OpenDialogues(Message[] messages, Actor[] actors)
    {
        InitializeActorNameColors();
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        dialogueAnimation.SetBool("DialogueIsActive", true);
        arrowAnimation.SetBool("DialogueIsActive", true);
        DisplayActorInfo();
    }

    public void OpenDialoguesSetting(Message[] messages, Actor[] actors)
    {
        Debug.Log("coco");
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        DisplayActorInfo();
        StartDisplayingMessage();
    }

    //use for dialoguebox animation
    public void StartDisplayingMessage()
    {
        DisplayMessageText();
    }

    private void DisplayMessageText()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        if(displayLineCoroutine != null )
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(messageToDisplay.message));
    }

    private void DisplayActorInfo()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorImage.sprite = actorToDisplay.sprite;
        actorName.text = actorToDisplay.name;
        if (actorNameColors.ContainsKey(messageToDisplay.actorID))
        {
            actorName.color = actorNameColors[messageToDisplay.actorID];
        }
        else
        {
            actorName.color = Color.black; 
        }
    }

    private IEnumerator CanSkip()
    {
        canSkip = false;
        yield return new WaitForSeconds(typingSpeed);
        canSkip = true;
    }

    private IEnumerator DisplayLine(string line)
    {
        messageText.text = "";

        submitSkip = false;
        StartCoroutine(CanSkip());

        foreach (char letter in line.ToCharArray())
        {
            if(canSkip && submitSkip)
            {
                submitSkip = false;
                messageText.text = line;
                break;
            }
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        canSkip = false;
    }

    public void NextMessage()
    {
        activeMessage++;

        if (activeMessage < currentMessages.Length)
        {
            DisplayActorInfo();
            DisplayMessageText();
        }
        else
        {
            isActive = false;
            dialogueAnimation.SetBool("DialogueIsActive", false);
            arrowAnimation.SetBool("DialogueIsActive", false);
            messageText.text = "";
        }
    }

    public void Dialogue(InputAction.CallbackContext context)
    {
        if (isActive && context.performed && transform.position.y > 169.9)
        {
            if (canSkip)
                submitSkip = true;
            else if(!canSkip)
                NextMessage();
        }
    }
}