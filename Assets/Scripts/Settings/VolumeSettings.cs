using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        sfxToggle.onValueChanged.AddListener(ToggleSFX);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicToggle.isOn = PlayerPrefs.GetInt("MusicToggle", 1) == 1;
        sfxToggle.isOn = PlayerPrefs.GetInt("SFXToggle", 1) == 1;
    }

    void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        musicToggle.onValueChanged.RemoveListener(ToggleMusic);
        sfxToggle.onValueChanged.RemoveListener(ToggleSFX);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicToggle.isOn)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetFloat("MusicVolume", 0);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxToggle.isOn)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", -80);
            PlayerPrefs.SetFloat("SFXVolume", 0);
        }
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
            PlayerPrefs.SetInt("MusicToggle", 1);
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", -80);
            PlayerPrefs.SetInt("MusicToggle", 0);
        }
    }

    public void ToggleSFX(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
            PlayerPrefs.SetInt("SFXToggle", 1);
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", -80);
            PlayerPrefs.SetInt("SFXToggle", 0);
        }
    }
}