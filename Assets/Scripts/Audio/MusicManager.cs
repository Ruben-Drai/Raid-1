using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    public Slider volumeMusicSlider;
    public Image MusicImage;
    public Sprite mutedMusicSprite;
    public Sprite notMutedMusicSprite;

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
        GameObject.Find("Music").GetComponent<AudioSource>().volume = volumeMusicSlider.value;
        Save();
    }
    public void Load()
    {
        volumeMusicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        GameObject.Find("Music").GetComponent<AudioSource>().volume = volumeMusicSlider.value;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeMusicSlider.value);
    }

    private void Update()
    {
        if (volumeMusicSlider != null)
            if (volumeMusicSlider.value > 0)
                MusicImage.sprite = notMutedMusicSprite;
            else if (volumeMusicSlider.value == 0)
                MusicImage.sprite = mutedMusicSprite;
    }
}
