using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }



    [Header("Audio Clips")]
    [SerializeField] private AudioClip laserAudioClip;
    [SerializeField] private AudioClip explosionAudioClip;

    [SerializeField] private AudioClip musicAudioClip;




    [Header("Audio Sources")]
    [SerializeField] private AudioSource laserAudioSource;
    [SerializeField] private AudioSource musicAudioSource;



    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

    }

    void Update()
    {
        PlayMusic();
    }



    public void PlayLaserAudioClip()
    {
        laserAudioSource.PlayOneShot(laserAudioClip);
    }


    public void PlayMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.PlayOneShot(musicAudioClip);
        }
    }


    public void PlayExplosionAudioClip()
    {
        laserAudioSource.PlayOneShot(explosionAudioClip);
    }
}
