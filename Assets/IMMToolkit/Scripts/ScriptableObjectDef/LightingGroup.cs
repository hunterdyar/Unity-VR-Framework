using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IMMToolkit{
[CreateAssetMenu(fileName = "LightGroup", menuName = "IMMToolkit/LightingGroup", order = 1)]
public class LightingGroup : ScriptableObject
{   
    float fadeOutTime = 1;
    private List<LightgroupMember> members;
    public void RegisterMember(LightgroupMember member){
        if(members == null)
        {
            members = new List<LightgroupMember>();
        }
        if(!members.Contains(member))
        {
            members.Add(member);
        }
        else
        {
            Debug.LogWarning("Audio Source already member of group: "+this.name,member);
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
    public void FadeLightsOut()
    {
        foreach(LightgroupMember l in members)
        {
            l.FadeOut(fadeOutTime);
        }
    }


}
}