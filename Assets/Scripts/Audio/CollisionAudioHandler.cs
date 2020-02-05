using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IMMToolkit{
public class CollisionAudioHandler : MonoBehaviour
{
    [Tooltip("Will play a random selection of listed clips. Using a single clip will have it just play that one every time")]
    public AudioClip[] sourceClips;
    [Tooltip("The magnitude of the relative velocity of impact (m/s) is multiplied by this number to set the volume for collisions. The result is clamped to between 0 and 1.")]
    public float collisionVolume = 1;
    private AudioSourceInterface audioInterface;
    private float defaultVolume;
    public bool ignoreGroupForCollisions;
    void Awake(){//Initialization
        audioInterface = GetComponent<AudioSourceInterface>();
        defaultVolume = audioInterface.GetVolume();
        //If it's empty, we should just use the audioSource clip instead.
        if(sourceClips.Length == 0 && audioInterface.GetClip() != null)
        {
            sourceClips = new AudioClip[1];
            sourceClips[0] = audioInterface.GetClip();
        }
        if(GetComponent<Rigidbody>() == null){
            Debug.LogWarning("Play on collisions is set but there is no rigidbody attached to this object. It still might work (?) but is probably not intended.",gameObject);
        }
    }
    void Start(){
        if(collisionVolume == 0)
        {
            Debug.LogWarning("Collision Volume Multiplier is 0. Volume of collision will always be zero.",this);
        }
    }
    public void ResetVolume(){
        audioInterface.ResetVolume();
    }

    void OnCollisionEnter(Collision col){
        float newVolume = Mathf.Clamp01(col.relativeVelocity.magnitude*collisionVolume);
        audioInterface.SetVolume(newVolume);
        audioInterface.SetClip(sourceClips[Random.Range(0,sourceClips.Length-1)]);
        if(ignoreGroupForCollisions)
        {
           audioInterface.ForcePlay(); 
        }else
        {
            audioInterface.Play();
        }
    }
}
}