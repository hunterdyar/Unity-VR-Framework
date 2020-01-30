using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudioHandler : MonoBehaviour
{
    [Tooltip("Will play a random selection of listed clips. Using a single clip will have it just play that one every time")]
    public AudioClip[] sourceClips;
    [Tooltip("The magnitude of the relative velocity of impact (m/s) is multiplied by this number to set the volume for collisions. The result is clamped to between 0 and 1.")]
    public float collisionVolume = 1;
    private AudioSource audioSource;
    private float defaultVolume;
    void Awake(){//Initialization
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
        //If it's empty, we should just use the audioSource clip instead.
        if(sourceClips.Length == 0 && audioSource.clip != null)
        {
            sourceClips = new AudioClip[1];
            sourceClips[0] = audioSource.clip;
        }
        if(GetComponent<Rigidbody>() == null){
            Debug.LogWarning("Play on collisions is set but there is no rigidbody attached to this object. It still might work but is probably not intended.",gameObject);
        }
    }
    void Start(){
        if(collisionVolume == 0)
        {
            Debug.LogWarning("Collision Volume Multiplier is 0 and playOnCollisions is 0. Volume of collision will always be zero.",gameObject);
        }
    }
    public void ResetVolume(){
        audioSource.volume = defaultVolume;
    }
    public void Play()
    {
        audioSource.clip = sourceClips[Random.Range(0,sourceClips.Length-1)];
        audioSource.Play();
    }

    void OnCollisionEnter(Collision col){
        float newVolume = Mathf.Clamp01(col.relativeVelocity.magnitude*collisionVolume);
        Play();
    }
}
