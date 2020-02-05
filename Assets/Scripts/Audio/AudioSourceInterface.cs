using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceInterface : MonoBehaviour
{
    [Header("Optional")]
    [Tooltip("The audio group will stop all other members of the group from playing audio, so you don't have overlapping sound from members of a group.")]
    public AudioGroup group;
    [HideInInspector]
    public AudioSource audioSource;
    private AudioClip defaultClip;
    private float defaultVolume;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        defaultClip = audioSource.clip;
        defaultVolume = audioSource.volume;
        if(group == null || audioSource == null)
        {
            Debug.LogError("Audio Group Interface not correctly set up.");
        }
    }

    public void Play()
    {
        if(group != null)
        {
            group.PlayOne(audioSource);
        }else
        {
            audioSource.Play();
        }
    }
    public void ForcePlay()//ignores the group
    {
        audioSource.Play();
    }
    public void SetClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }
    public AudioClip GetClip()
    {
        return audioSource.clip;
    }
    public float GetVolume()
    {
        return audioSource.volume;
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void StopAllMembers()
    {
        group.StopAll();
    }
    public void ResetVolume()
    {
        audioSource.volume = defaultVolume;
    }
    public void ResetClip()
    {
        audioSource.clip = defaultClip;
    }
    void OnEnable()
    { 
        group.RegisterMember(audioSource);

    }
    void OnDisable()
    {
        group.DeregisterMember(audioSource);
    }

    
}
