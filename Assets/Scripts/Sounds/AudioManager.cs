using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(0);
    }

    public void PlayMusic(int index)
    {
        if (index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();

        }
        else
        {
            Debug.LogWarning("Music index out of range: " + index);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();

    }

    public void PlaySFX(int index)
    {
        if (index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
        else
        {
            Debug.LogWarning("SFX index out of range: " + index);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
        Debug.Log("SFX stopped");
    }
}