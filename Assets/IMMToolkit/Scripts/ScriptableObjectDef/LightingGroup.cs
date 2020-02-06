using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
namespace IMMToolkit{
[CreateAssetMenu(fileName = "LightGroup", menuName = "IMMToolkit/LightingGroup", order = 1)]
public class LightingGroup : ScriptableObject
{   
    [Separator("Lightgroup Settings")]
    [Tooltip("Time in seconds to fade in and out.")]
    public float fadeOutTime = 1;
    private List<LightgroupMember> members;

    private void ClearMembers()
    {
        members.Clear();
    }
    public void RegisterMember(LightgroupMember member){
        if(members == null)//initialize if needed.
        {
            members = new List<LightgroupMember>();
        }
        if(!members.Contains(member))
        {
            members.Add(member);
        }
        else
        {
            // This actually happens a lot because scriptableObjects dont reset on exiting play mode.
            // Debug.LogWarning("Light already member of group: "+this.name,member);
        }
    }
    public void DeregisterMember(LightgroupMember member){
        if(members == null)
        {
            return;
        }
        if(members.Contains(member))
        {
            members.Remove(member);
        }
    }
    [ButtonMethod]
    public void FadeLightsOut()
    {
        foreach(LightgroupMember l in members)
        {
            if(l == null)
            {
                Debug.Log("???");
            }else
            {
                l.FadeOut(fadeOutTime);
            }
        }
    }
     [ButtonMethod]
    public void FadeLightsIn()
    {
        foreach(LightgroupMember l in members)
        {
            if(l == null)
            {
                Debug.Log("???");
            }else
            {
                l.FadeIn(fadeOutTime);
            }
        }
    }

}
}