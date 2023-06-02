using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] Text text;
    string writer;
    [SerializeField] private Coroutine coroutine;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [Space(10)][SerializeField] private bool startOnEnable = false;

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
        if (startOnEnable) StartTypewriter();
    }

    private void StartTypewriter()
    {
        StopAllCoroutines();

        if (text != null)
        {
            text.text = "";

            StartCoroutine("TypeWriterText");
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
            yield return new WaitForSeconds(timeBtwChars);
        }
        yield return null;
    }
}