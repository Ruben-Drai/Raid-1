using System;
using UnityEngine;

public class Animals_Controller : MonoBehaviour
{
    //Minimum & Maximum variable = Mm
    [Header("Min & Max")]
    public Vector2 distanceMm = new Vector2(0, 10);
    public Vector2 waitTimeMm = new Vector2(1, 10);

    [Header("Animal Information")]
    [Range(0, 10)] public float speed = 1.0f;
    public int toDo = 0; //What the animals need to do (percentage(0-99) of chance to do an action)
    public Vector2 startEndPos = new Vector2(0, 0); // startEndPos [0] Start position, startEndPos [1] distance in x axis,
    private int direction = 0; // 0 = right and 1 =  left


    [Header("Flee")]
    public bool isBird = true;
    public float disappearSpeed = 7;
    private bool flee = false;
    private float fleeTimer = 0;

    //GameObject
    private GameObject eye;
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rb;

    //Timer
    public float waitTime = 0; // Time to wait during the idle event
    public float timer = 0;
    private int pastTime = 0; // Use for make disappear the animal during the flee

    //LayerMask
    private RaycastHit2D eyeRay; // see in direction of the player for know if the animal need to flee
    private RaycastHit2D obstacle; // see if there is obstacle front of animal direction

    //Audio
    private float soundTimer = 0;
    private float waitSound;



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
        Sound();
        if (!flee)
        {
            fleeTimer = Time.time;
            Vector2 heading = player.transform.position - eye.transform.position;

            eyeRay = Physics2D.Raycast(eye.transform.position, heading.normalized, 5f, LayerMask.GetMask("Player"));

            if (eyeRay.collider != null && eyeRay.collider.CompareTag("Player"))
            {
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

                obstacle = Physics2D.Raycast(eye.transform.position, gameObject.transform.right, 2f, LayerMask.GetMask("Player"));

                if (Mathf.Abs(transform.position.x - startEndPos.x) > startEndPos.y || obstacle.collider != null)
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
            if (Time.time - fleeTimer > disappearSpeed)
            {
                Destroy(gameObject);
            }
            else if (Time.time - fleeTimer > pastTime)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, gameObject.GetComponent<SpriteRenderer>().color.a - 1f / disappearSpeed);
                pastTime++;
            }

        }
    }

    private int Movement() //  for know the animation to play

    {
        int a = UnityEngine.Random.Range(0, 100);
        if (a > 50)
        {
            direction = UnityEngine.Random.Range(0, 2);
            startEndPos.x = transform.position.x;
            startEndPos.y = UnityEngine.Random.Range(distanceMm.x, distanceMm.y);
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

    private void Sound()
    {
        if (Time.time - soundTimer > waitSound)
        {
            soundTimer = Time.time;
            waitSound = UnityEngine.Random.Range(0, 20);
            GetComponent<AudioSource>().Play();
        }

    }

    private void BirdFlee()
    {
        animator.SetBool("Move", true);
        if (player.transform.position.x - transform.position.x < 0)
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

    public void Death() // Using in the Animation of the death for make disappear the animal
    {
        Destroy(gameObject);
    }
}
