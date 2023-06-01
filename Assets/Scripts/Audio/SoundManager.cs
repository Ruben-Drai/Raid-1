using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider volumeSoundSlider;
    public Image SoundImage;
    public Sprite mutedSoundSprite;
    public Sprite lowSoundSprite;
    public Sprite loudSoundSprite;
    public Sprite middleSoundSprite;
    public AudioSource Back;
    public AudioSource Click;

    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Load();
    }
    public void ChangeVolume()
    {
        foreach (var v in FindObjectsOfType<AudioSource>())
        {
            if (v.gameObject.name != "Music")
                v.volume = volumeSoundSlider.value;
        }
        Save();
    }
    
    public void Load()
    {
        volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);
        ChangeVolume();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
    }

    void Update()
    {
        if (volumeSoundSlider != null)
            if (volumeSoundSlider.value == 0)
                SoundImage.sprite = mutedSoundSprite;
            else if (volumeSoundSlider.value > 0 && volumeSoundSlider.value < 0.33)
                SoundImage.sprite = lowSoundSprite;
            else if (volumeSoundSlider.value < 0.66)
                SoundImage.sprite = middleSoundSprite;
            else if (volumeSoundSlider.value == 1)
                SoundImage.sprite = loudSoundSprite;
    }
}
