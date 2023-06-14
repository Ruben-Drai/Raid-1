using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class TypingEffect : MonoBehaviour
{
    public Text text;
    [SerializeField] private Coroutine coroutine;
    [Space(10)][SerializeField] private bool startOnEnable = false;
    [SerializeField] private float timeBtwChars = 0.1f;

    private bool isTyping = false;
    private bool ShowText = false;
    private string writer;

    public static bool canSkip = false;
    public static float delayBeforeStart = 4f;

    enum options { clear, complete }

    void Awake()
    {
        if (text != null)
        {
            writer = text.text;
        }
    }
    
    void Start()
    {
        if (text != null)
        {
            text.text = "";
        }
    }
    private void OnEnable()
    {
        ShowText = true;
    }


    private void Update()
    {
        if (startOnEnable && ShowText)
        {
            StartTypewriter();
            ShowText = false;
        }
    }
    private void StartTypewriter()
    {
        StopAllCoroutines();

        if (text != null)
        {
            text.text = "";
            isTyping = true;
            coroutine = StartCoroutine("TypeWriterText");
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        text.text = "";
    }

    IEnumerator TypeWriterText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (text.text.Length > 0)
            {
                text.text = text.text.Substring(0, text.text.Length);
            }
            text.text += c;
            isTyping = true;
            yield return new WaitForSeconds(timeBtwChars);
        }
        yield return null;
    }

    public void DisplayText(InputAction.CallbackContext context)
    {
        if(context.performed && context.control.path != "/Mouse/leftButton" && isTyping && canSkip) 
        {
            StopCoroutine(coroutine);
            text.text = writer;
        }
    }
}