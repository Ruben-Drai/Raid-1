using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSoundSlider;
    public Image mutedSoundImage;

    public Sprite mutedSoundSprite;
    public Sprite lowSoundSprite;
    public Sprite loudSoundSprite;
    public Sprite middleSoundSprite;

    //[SerializeField] AudioSource Sound;
    void Start()
    {
        Load();
    }
    public void ChangeVolume()
    {
        //Sound.volume = volumeSoundSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSoundSlider.value = PlayerPrefs.GetFloat("soundVolume", 0.5f);
    }


    private void Save()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSoundSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeSoundSlider.value == 0)
        {
            mutedSoundImage.sprite = mutedSoundSprite;
        }
        else if(volumeSoundSlider.value > 0 && volumeSoundSlider.value < 0.33)
        {
            mutedSoundImage.sprite = lowSoundSprite;
        }
        else if(volumeSoundSlider.value < 0.66)
        {
            mutedSoundImage.sprite = middleSoundSprite;
        }
        else if (volumeSoundSlider.value < 1)
        {
            mutedSoundImage.sprite = loudSoundSprite;
        }
    }
}
