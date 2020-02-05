using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSourceInterface))]
public class PlayOnceAudioHandler : MonoBehaviour
{
    [Tooltip("Optional. Define the clip here or just in the audio source component.")]
    public AudioClip sourceClip;
    private AudioSourceInterface audioInterface;
    private bool played = false;
    public bool dontCountAsPlayedUntilClipFinishes = false;
    void Start()
    {
        audioInterface = GetComponent<AudioSourceInterface>();
        
        #region Tests
        if(audioInterface.audioSource.playOnAwake)
        {
            Debug.LogWarning("Audio source for play once handler is set to play on awake. This is probably unintentional.");
        }
        #endregion
        
        //Option clip override.
        if(sourceClip != null)
        {
            audioInterface.SetClip(sourceClip);
        }
    }
    public void Play()
    {
        if(played == false)
        {
            audioInterface.Play();
            if(!dontCountAsPlayedUntilClipFinishes)
            {
                played = true;
            }
            else
            {
                StartCoroutine(WaitForEndToSetPlayed(audioInterface.GetClip()));
            }
        }
    }
    public void Reset()
    {
        played = false;
    }
    IEnumerator WaitForEndToSetPlayed(AudioClip startedClip)
    {
        AudioClip shouldPlayClip = startedClip;//This is the clip that should still be playing.
        yield return new WaitForSeconds(audioInterface.audioSource.clip.length - 0.1f);//-0.1f to check if it is playing just before it ends.
        //if the source is playing the same clip, and is still playing it, just before the clip should be ending.
        if(shouldPlayClip == startedClip && audioInterface.audioSource.isPlaying)//if we didn't get stopped at some other point.
        {
            played = true;
        }
        
    }
}
