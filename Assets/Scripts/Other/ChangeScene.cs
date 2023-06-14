using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string LevelToLoad;

    private bool changeScene = false;
    [SerializeField] private float transitionSpeed = 0.05f;
    [SerializeField] public SpriteRenderer ocultation;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(changeScene)
            if(Occultation())
            {
                SceneManager.LoadScene(LevelToLoad);
            }
                

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            changeScene = true;
        }
    }

    private bool Occultation()
    {
        if (ocultation.color.a >= 1)
            return true;
        if( Time.time - timer > 0.01f)
        {
            ocultation.color = new Color(ocultation.color.r, ocultation.color.g, ocultation.color.b, ocultation.color.a + transitionSpeed);
            timer = Time.time;
        }
            
        
        return false;
    }
}
