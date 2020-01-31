using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnceAudioHandler : MonoBehaviour
{
    [Tooltip("Optional. Define the clip here or just in the audio source component.")]
    public AudioClip sourceClip;
    private AudioSource audioSource;

    private bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        #region Tests
        if(audioSource.playOnAwake)
        {
            Debug.LogWarning("Audio source for play once handler is set to play on awake. This is probably unintentional.");
        }
        #endregion
        
        if(sourceClip != null)
        {
            audioSource.clip = sourceClip;
        }
    }
    public void Play()
    {
        if(played == false)
        {
            audioSource.Play();
            played = true;
        }
    }
    public void Reset()
    {
        played = false;
    }
}
