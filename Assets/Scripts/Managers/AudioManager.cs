using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }



    [Header("Audio Clips")]
    [SerializeField] private AudioClip laserAudioClip;
    [SerializeField] private AudioClip playerExplosionAudioClip;

    [SerializeField] private AudioClip enemyLaserAudioClip;

    [SerializeField] private AudioClip enemyExplosionAudioClip;


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



    public void PlayPlayerLaserAudioClip()
    {
        laserAudioSource.PlayOneShot(laserAudioClip);
    }


    public void PlayEnemyLaserAudioClip()
    {
        laserAudioSource.PlayOneShot(enemyLaserAudioClip);
    }


    public void PlayMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.PlayOneShot(musicAudioClip);
        }
    }


    public void PlayEnemyExplosionAudioClip()
    {
        laserAudioSource.PlayOneShot(enemyExplosionAudioClip);
    }


    public void PlayPlayerExplosionAudioClip()
    {
        laserAudioSource.PlayOneShot(playerExplosionAudioClip);
    }
}
