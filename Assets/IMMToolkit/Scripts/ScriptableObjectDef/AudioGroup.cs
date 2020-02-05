using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IMMToolkit{
[CreateAssetMenu(fileName = "Group", menuName = "IMMToolkit/AudioGroup", order = 1)]
public class AudioGroup : ScriptableObject
{   
    public bool fadeInsteadOfStopping;
    [Tooltip("Percent of default volume to fade down (or up?) to.")]
    public float fadeAmount;
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
                //Fade volume down?
                //Should audioGroups be lists of the interfaces, not the group?
                //dang. yeah.
               // source.volume
            }
        }
    }
    public void PlayOne(AudioSourceInterface audioInterface)
    {
        if(members.Contains(audioInterface)){
            StopAll();
            audioInterface.Play();
        }else{
            Debug.LogWarning("audio source not member of group.",audioInterface);
        }
    }
}
}