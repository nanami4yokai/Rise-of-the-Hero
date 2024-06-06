using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AudioClipInfo
{
    public AudioClip clip;
    public float volume = 1.0f;
    public bool loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusicAudioSource;
    public AudioSource burningFireAudioSource;
    public AudioSource oneOffAudioSource;

    public Slider volumeSlider;

    public AudioClipInfo bgMusic;
    public AudioClipInfo burningFire;
    public AudioClipInfo drink;
    public AudioClipInfo gearUp;
    public AudioClipInfo pickUpLoot;
    public AudioClipInfo playerDeath;
    public AudioClipInfo swordSwing;
    public AudioClipInfo teleport;
    public AudioClipInfo walking;
    public AudioClipInfo openChest;
    public AudioClipInfo openGate;
    public AudioClipInfo monsterDeath;
    public AudioClipInfo bossFight;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            PlayLoopingAudio(bgMusic, bgMusicAudioSource);
            PlayLoopingAudio(burningFire, burningFireAudioSource);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    public void SetMasterVolume(float volume)
    {
        bgMusicAudioSource.volume = volume;
        burningFireAudioSource.volume = volume;
        oneOffAudioSource.volume = volume;
    }

    public void PlayAudio(AudioClipInfo audioClipInfo)
    {
        if (audioClipInfo != null && audioClipInfo.clip != null)
        {
            oneOffAudioSource.PlayOneShot(audioClipInfo.clip, audioClipInfo.volume);
        }
    }

    public void PlayLoopingAudio(AudioClipInfo audioClipInfo, AudioSource audioSource)
    {
        if (audioClipInfo != null && audioClipInfo.clip != null && audioSource != null)
        {
            audioSource.clip = audioClipInfo.clip;
            audioSource.volume = audioClipInfo.volume;
            audioSource.loop = audioClipInfo.loop;
            audioSource.Play();
        }
    }

    public void StopLoopingAudio(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        bgMusicAudioSource.volume = volume;
        burningFireAudioSource.volume = volume;
        oneOffAudioSource.volume = volume;
    }

    public void PlayWalkingSound()
    {
        if (walking != null && walking.clip != null)
        {
            if (!bgMusicAudioSource.isPlaying)
            {
                PlayLoopingAudio(bgMusic, bgMusicAudioSource);
            }
            PlayLoopingAudio(walking, oneOffAudioSource);
        }
    }
}
