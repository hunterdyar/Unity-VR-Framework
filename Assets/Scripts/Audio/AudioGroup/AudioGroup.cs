using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group", menuName = "IMMToolkit/AudioGroup", order = 1)]
public class AudioGroup : ScriptableObject
{   
    private List<AudioSource> members;
    public void RegisterMember(AudioSource source){
        if(members == null)
        {
            members = new List<AudioSource>();
        }
        if(!members.Contains(source))
        {
            members.Add(source);
        }
        else
        {
            Debug.LogWarning("Audio Source already member of group: "+this.name,source);
        }
    }
    public void DeregisterMember(AudioSource source){
        if(members == null)
        {
            return;
        }
        if(members.Contains(source))
        {
            members.Remove(source);
        }
    }
    public void StopAll()
    {
        foreach(AudioSource source in members)
        {
            if(source.enabled && source.gameObject.activeInHierarchy)
            {
                source.Stop();
            }
        }
    }
    public void PlayOne(AudioSource source)
    {
        if(members.Contains(source)){
            StopAll();
            source.Play();
        }else{
            Debug.LogWarning("audio source not member of group.",source);
        }
    }
}
