using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Animals_Controller : MonoBehaviour
{
    //Minimum & Maximum variable = Mm
    public bool isBird = true;
    private Animator animator;
    private Rigidbody2D rb;
    public float speed = 1.0f;
    public Vector2 distanceMm = new Vector2(0,10);
    public Vector2 waitTimeMm = new Vector2( 1, 10 );
    public int toDo = 0;
    public float waitTime = 0;
    private int direction = 0; // 0 = right and 1 =  left
    public Vector2 distance = new Vector2(0,0); // distance to travel when GameObject move, with x the start and y the distance to travel
    public float timer = 0;
    private GameObject eye;
    private GameObject player;
    private RaycastHit2D eyeRay;
    public LayerMask layerMask;
    private bool flee = false;
    private float fleeTimer = 0;
    private RaycastHit2D obstacle;
    private int pastTime = 0;
    public float disappearSpeed = 7;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        toDo = Movement();
        timer = Time.time;
        eye = transform.Find("eye").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flee)
        {
            fleeTimer = Time.time;
            Vector3 heading = player.transform.position - eye.transform.position;
            Debug.DrawRay(eye.transform.position, heading / heading.magnitude * 5, Color.red);
            eyeRay = Physics2D.Raycast(eye.transform.position, heading / heading.magnitude, 5f, layerMask);

            if (eyeRay.collider != null && eyeRay.collider.tag == "Player")
            {
                Debug.DrawRay(eye.transform.position, heading / heading.magnitude * 5, Color.green);
                flee = true;
            }

            if (toDo > 50)
            {
                if (direction == 0)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                if(Mathf.Abs(rb.velocity.x) <= 0.01f)
                {

                }
                Debug.DrawRay(eye.transform.position, gameObject.transform.right * 0.5f, Color.red);
                obstacle = Physics2D.Raycast(eye.transform.position, gameObject.transform.right, 0.5f, layerMask);
                if (Mathf.Abs(transform.position.x - distance.x) > distance.y || obstacle.collider != null)
                {
                    toDo = Movement();
                }

            }
            else
            {
                if (Time.time - timer > waitTime) { toDo = Movement(); }

            }
        }
        else
        {
            if (isBird)
                BirdFlee();
            else
                RatFlee();
            if(Time.time - fleeTimer > disappearSpeed)
            {
                Destroy(gameObject);
            }
            else if(Time.time - fleeTimer > pastTime)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, gameObject.GetComponent<SpriteRenderer>().color.a - 1f/disappearSpeed);
                pastTime++;
            }

        }
    }

    private int Movement()

    {
        int a = UnityEngine.Random.Range(0, 100);
        if (a > 50)
        {
            direction = UnityEngine.Random.Range(0, 2);
            distance.x = transform.position.x;
            distance.y = UnityEngine.Random.Range(distanceMm.x, distanceMm.y);
            animator.SetBool("Move", true);

        }
        else
        {
            waitTime = UnityEngine.Random.Range(waitTimeMm.x, waitTimeMm.y);
            timer = Time.time;
            animator.SetBool("Move", false);
        }
        return a;
    }

    private void BirdFlee()
    {
        animator.SetBool("Move", true);
        if(player.transform.position.x - transform.position.x < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = new Vector2(5, 5);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            rb.velocity = new Vector2(-5, 5);
        }
           
    }

    private void RatFlee() 
    {
        animator.SetBool("Move", true);
        if (player.transform.position.x - transform.position.x < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.velocity = new Vector2(5, rb.velocity.y);
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
           
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
