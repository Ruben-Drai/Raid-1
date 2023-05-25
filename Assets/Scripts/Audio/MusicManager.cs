using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider volumeMusicSlider;
    public Image mutedMusicImage;

    public Sprite mutedMusicSprite;
    public Sprite notMutedMusicSprite;
    //[SerializeField] AudioSource Music;
    void Start()
    {
        Load();    
    }

    public void ChangeVolume()
    {
        //Music.volume = volumeMusicSlider.value;
        Save();
    }

    private void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
    }

    private void Update()
    {
        if (volumeMusicSlider.value > 0)
        {
            mutedMusicImage.sprite = notMutedMusicSprite;
        }
        else if(volumeMusicSlider.value == 0)
            mutedMusicImage.sprite = mutedMusicSprite;
    }
}
