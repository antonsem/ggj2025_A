using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    //Other Settings...

    // Default values
    private float defaultVolume = 1f;


    void Start()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {

        }
        SetSoundVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSoundVolume()
    {
        float volume = volumeSlider.value;


    }


}
