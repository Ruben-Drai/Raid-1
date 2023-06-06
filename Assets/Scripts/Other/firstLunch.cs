using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstLunch : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = 0.05f;
    private float timer;
    private SpriteRenderer ocultation;
    private bool _firstLunch = true;
    // Start is called before the first frame update
    void Start()
    {
     ocultation = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (_firstLunch)
            if (Occultation())
                _firstLunch = false;
    }

    private bool Occultation()
    {
        if (ocultation.color.a <= 0)
            return true;
        if (Time.time - timer > 0.1f)
        {
            ocultation.color = new Color(ocultation.color.r, ocultation.color.g, ocultation.color.b, ocultation.color.a - transitionSpeed);
            timer = Time.time;
        }


        return false;
    }
}
