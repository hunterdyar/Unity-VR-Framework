using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
namespace IMMToolkit{
[CreateAssetMenu(fileName = "Group", menuName = "IMMToolkit/AudioGroup", order = 1)]
public class AudioGroup : ScriptableObject
{   
    public bool fadeInsteadOfStopping;
    // [Tooltip("Percent of default volume to fade down (or up?) to.")]
    [MyBox.ConditionalField(nameof(fadeInsteadOfStopping))]
    [Range(0,1)]
    [Tooltip("Factor multiplied to volume. .2 = 20% of 'normal' volume.")]
    public float fadeFactor;
    [MyBox.ConditionalField(nameof(fadeInsteadOfStopping))]
    [Tooltip("In Seconds")]
    public float fadeDuration;
    private List<AudioSourceInterface> members;
    public void RegisterMember(AudioSourceInterface audioInterface){
        if(members == null)
        {
            members = new List<AudioSourceInterface>();
        }
        if(!members.Contains(audioInterface))
        {
            members.Add(audioInterface);
        }
        else
        {
            Debug.LogWarning("Audio Source already member of group: "+this.name,audioInterface);
        }
    }
    public void DeregisterMember(AudioSourceInterface audioInterface){
        if(members == null)
        {
            return;
        }
        if(members.Contains(audioInterface))
        {
            members.Remove(audioInterface);
        }
    }
    public void StopAll()
    {
        foreach(AudioSourceInterface audioInterface in members)
        {
            if(audioInterface.gameObject.activeInHierarchy)
            {
                audioInterface.Stop();
            }
        }
    }
    public void FadeAllDown()
    {
        foreach(AudioSourceInterface audioInterface in members)
        {
            if(audioInterface.gameObject.activeInHierarchy)
            {
                audioInterface.StartCoroutine(audioInterface.FadeVolumeByFactor(fadeFactor,fadeDuration));
            }
        }
    }
    public void FadeAllUp()
    {
        foreach(AudioSourceInterface audioInterface in members)
        {
            if(audioInterface.gameObject.activeInHierarchy)
            {
                audioInterface.StartCoroutine(audioInterface.FadeVolumeByFactor(1,fadeDuration));//1 = default volume.
            }
        }
    }
    public void PlayOne(AudioSourceInterface audioInterface)
    {
        if(fadeInsteadOfStopping)
        {
            if(members.Contains(audioInterface)){
                foreach(AudioSourceInterface asi in members)
                {
                    if(audioInterface.gameObject.activeInHierarchy && asi != audioInterface)//hey, don't fade down the one thing we WANT to play.
                    {
                        asi.StartCoroutine(asi.FadeVolumeByFactor(fadeFactor,fadeDuration));//fade out
                        //fade back in after the length of time of the - hopefully - clip that is playing.
                        asi.StartCoroutine(asi.FadeVolumeByFactor(1,fadeDuration,audioInterface.audioSource.clip.length));
                    }
                }
                audioInterface.ForcePlay();//play this clip at the appropriate time.
            }
        }else{
            if(members.Contains(audioInterface)){
                StopAll();
                audioInterface.ForcePlay();
            }else{
                Debug.LogWarning("audio source not member of group.",audioInterface);
            }
        }
    }
}
}