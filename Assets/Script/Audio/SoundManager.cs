using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        soundSource = GetComponent<AudioSource>();
        if (transform.childCount > 0)
        {
            musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        }

        //Assign initial volumes
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        if (soundSource == null || _sound == null)
        {
            return;
        }

        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
        

    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(1, "musicVolume", _change, musicSource);
    }

    public void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        if (source == null)
        {
            return;
        }

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat(volumeName, currentVolume);

        PlayerPrefs.SetFloat("externalSoundVolume", soundSource != null ? soundSource.volume : 0f);
        PlayerPrefs.SetFloat("externalMusicVolume", musicSource != null ? musicSource.volume : 0f);
        PlayerPrefs.Save();
    }
}
