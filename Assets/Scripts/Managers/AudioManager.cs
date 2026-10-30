using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set;}



    [Header("Audio Clips")]
    [SerializeField] private AudioClip laserAudioClip;




    [Header("Audio Sources")]
    [SerializeField] private AudioSource laserAudioSource;


    void Start()
    {
        if(instance != null && instance != this )
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
        
    }



    public void PlayLaserAudioClip()
    {
            laserAudioSource.PlayOneShot(laserAudioClip);
    }
}
