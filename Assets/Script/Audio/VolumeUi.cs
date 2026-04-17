using UnityEngine;
using UnityEngine.UI;

public class VolumeUi : MonoBehaviour
{
    [SerializeField] private Image volumeCurrent;
    [SerializeField] private Image musicCurrent;

    private void Update()
    {
      UpdateVolume();  
    }
    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat("externalSoundVolume", 1);
        float musicValue = PlayerPrefs.GetFloat("externalMusicVolume", 1);
        if (volumeCurrent != null)
        {
            volumeCurrent.fillAmount = volumeValue;
        }

        if (musicCurrent != null)
        {
            musicCurrent.fillAmount = musicValue;
        }
    }
}
