using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    public static float typingSpeed = 0.04f;

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

    Message[] currentMessages;
    Actor[] currentActors;

    public Slider speedText;
    public static bool isActive = false;
    public DialogueTrigger dialogueTrigger;

    [HideInInspector] public bool finishedAnimationUp = false;
    [HideInInspector] public bool finishedAnimationDown = false;
    
    private bool canSkip = false;
    private bool submitSkip;
    private Coroutine displayLineCoroutine;
    private Dictionary<int, Color> actorNameColors = new Dictionary<int, Color>();

    int activeMessage = 0;

    //This switch allows the slider to change the speed of the text according to its value. It also calls "DialogueSetting", which resets the text.
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

    private void Awake()
    {
        InitializeActorNameColors();
    }

    //This method changes the colour of the actor's name according to its index in a dictionary.
    //The actors are the characters who speak in the dialogue.
    public void InitializeActorNameColors()
    {
        actorNameColors.Add(0, robot); 
        actorNameColors.Add(1, narator); 
        actorNameColors.Add(2, system); 
        actorNameColors.Add(3, ai); 
    }

    public void OpenDialogues(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        dialogueAnimation.SetBool("DialogueIsActive", true);
        arrowAnimation.SetBool("DialogueIsActive", true);
        DisplayActorInfo();
    }

    //This method launches the dialogue in settings. It launches the animations, initializes the actors: their names, sprites and messages.
    public void OpenDialoguesSetting(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        DisplayActorInfo();
        StartDisplayingMessage();
    }

    //This method is called at the end of the dialogue box animation. 
    public void StartDisplayingMessage()
    {
        DisplayMessageText();
        finishedAnimationUp = true;

    }

    public void EndAnimation()
    {
        finishedAnimationDown = true;
    }

    //This method displays the dialogue text at the end of the animation. It also launches the coroutine to display the message letter by letter.
    private void DisplayMessageText()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        if(displayLineCoroutine != null )
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(messageToDisplay.message));
    }

    //This method configures the players. Their names and sprites and sets the default colour to white if no colour is specified.
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
            actorName.color = Color.white; 
        }
    }

    //This coroutine allows you to display the message all at once without waiting for each letter to be displayed.
    private IEnumerator CanSkip()
    {
        canSkip = false;
        yield return new WaitForSeconds(typingSpeed);
        canSkip = true;
    }

    //This coroutine displays the message letter by letter.
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
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        canSkip = false;
    }

    //On pressed escape or left click 
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
            finishedAnimationUp = false;
            finishedAnimationDown = false;
            messageText.text = "";
        }
    }

    public void ControlsDialogue(InputAction.CallbackContext context)
    {
        if (isActive && context.performed && finishedAnimationUp)
        {
            if (canSkip)
                submitSkip = true;
            else if(!canSkip)
                NextMessage();
        }
    }
}