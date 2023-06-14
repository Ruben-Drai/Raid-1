using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Image soundImage;

    // Start is called before the first frame update
    void Start()
    {
        soundSlider.onValueChanged.AddListener(delegate { SoundManager.instance.ChangeVolume(); });
        if(SoundManager.instance != null)
        {
            SoundManager.instance.volumeSoundSlider = soundSlider;
            SoundManager.instance.SoundImage = soundImage;
            SoundManager.instance.Load();
        }
        
    }


}
