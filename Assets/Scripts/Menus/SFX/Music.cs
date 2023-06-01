using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Image musicImage;
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate{ MusicManager.instance.ChangeVolume();});
        MusicManager.instance.volumeMusicSlider = musicSlider;
        MusicManager.instance.MusicImage = musicImage;
        MusicManager.instance.Load();
    }

}
