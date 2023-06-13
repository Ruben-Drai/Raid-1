using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Battery")]
    [SerializeField] private GameObject battery;
    [SerializeField] private TextMeshProUGUI chargeText;

    private Image batteryImage;
    public static float charge = 100f;

    [Header("Boss Timer")]
    [SerializeField] TextMeshProUGUI timerTxt;
    public int drainSpeed = 3;

    private static float timer;
    private static float batteryTimer = 0f;

    public static GameUI instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        batteryImage = battery.GetComponent<Image>();

        /* Display the battery image and it's value */
        batteryImage.fillAmount = charge / 100;
        chargeText.text = Mathf.FloorToInt(charge).ToString() + "%";

        timer = charge * drainSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (charge <= 0)
        {
            charge = 0f;
            PlayerShutDown();
        }

        //if bossfight?
        if (SceneManager.GetActiveScene().name == "Boss")
        {
            AutoBatteryDrain();
            BossTimerDisplay();
        }

        if(timer <= 20f)
        {
            timerTxt.color = new Color32(255, 21, 0, 255);
        }
    }

    /* The gameOver ("bad end") screen will be displayed */
    void PlayerShutDown()
    {
        Debug.Log("You lost !");
    }

    /* Drain the desired amount of battery when passing a check point, or while in the boss room */
    public void BatteryDrain(int value)
    {
        charge -= value;

        timer = charge * drainSpeed;

        batteryImage.fillAmount = charge / 100;
        chargeText.text = Mathf.FloorToInt(charge).ToString() + "%";
    }

    /* The battery will drain automatically */
    void AutoBatteryDrain()
    {
        if (batteryTimer >= drainSpeed)
        {
            BatteryDrain(1);
            batteryTimer = 0;
        }
        else
            batteryTimer += Time.deltaTime;
    }

    /* Display of the time remaining before the battery is empty */
    void BossTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float secondes = Mathf.FloorToInt(timer % 60);
        float millisecondes = Mathf .FloorToInt(timer * 1000);
        millisecondes = millisecondes % 1000;

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("You lost"); //TODO : put the gameOver scene here
            timer = 0f;
        }

        /* Display the timer */
        if (timerTxt != null)
            timerTxt.text = string.Format("{0:00}:{01:00}:{2:000}", minutes, secondes, millisecondes);

    }
}
