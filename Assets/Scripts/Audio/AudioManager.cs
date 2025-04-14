using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //public AudioSource backgroundMusic;

    private void Start()
    {
        //PlayAudio(backgroundMusic);
    }
    
    public void PlayAudio(AudioSource audioSource)
    {
        audioSource.Play();
    }
}
