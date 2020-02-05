using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IMMToolkit{
[CreateAssetMenu(fileName = "LightGroup", menuName = "IMMToolkit/LightingGroup", order = 1)]
public class LightingGroup : ScriptableObject
{   
    private List<Light> members;
    public void RegisterMember(Light source){
        if(members == null)
        {
            members = new List<Light>();
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
    public void DeregisterMember(Light source){
        if(members == null)
        {
            return;
        }
        if(members.Contains(source))
        {
            members.Remove(source);
        }
    }
}
}