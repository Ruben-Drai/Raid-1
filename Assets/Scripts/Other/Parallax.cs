using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x; // Take X pos at the start.
        lenght = GetComponent<SpriteRenderer>().bounds.size.x; // Take.
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); // The future position of the pattern.
        float dist = (cam.transform.position.x * parallaxEffect); // The future X position of the layer

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z); // move the layer according to the parallaxEffect and camera position.

        if (temp > startpos + lenght) { startpos += lenght; } // Moves the object (background) to the right, adding 1x its width if the camera has exceeded 1 map pattern to the right.
        else if (temp < startpos - lenght) { startpos -= lenght; } // Moves the object (background) to the left, reducing its width by 1x if the camera has exceeded 1 map pattern to the left.

    }
}
