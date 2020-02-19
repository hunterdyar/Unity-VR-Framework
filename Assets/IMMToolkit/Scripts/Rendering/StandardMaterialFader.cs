using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMaterialFader : MonoBehaviour
{
    public float timeToFade = 1;
    MeshRenderer mr;

    [MyBox.ButtonMethod]
    public void TestConfig()
    {
        if(mr == null){mr = GetComponent<MeshRenderer>();}
        if(mr.sharedMaterial.color.a >0 && mr.sharedMaterial.color.a < 1){
            Debug.LogWarning("Material on fade object not set to 0 or 1. This value will not be remembered after fading.",mr.material);
        }
    }

    [MyBox.ButtonMethod]
    public void FadeOut()
    {
        if(mr == null){mr = GetComponent<MeshRenderer>();}
        Color startColor = mr.material.color;
        Color endColor = new Color(startColor.r,startColor.g,startColor.b,0);
        StartCoroutine(DoFade(startColor,endColor,timeToFade));
    }
    [MyBox.ButtonMethod]
    public void FadeIn()
    {
        if(mr == null){mr = GetComponent<MeshRenderer>();}
        Color startColor = mr.material.color;
        Color endColor = new Color(startColor.r,startColor.g,startColor.b,1);
        StartCoroutine(DoFade(startColor,endColor,timeToFade));
    }
    IEnumerator DoFade(Color startColor, Color endColor, float timeToFade)
    {
        float t = 0;
        while(t<1)
        {
            foreach(Material m in mr.materials){
                m.color = Color.Lerp(startColor,endColor,t);
                t = t + Time.deltaTime/timeToFade;
            }
            yield return null;//waits after the loop so that they fade in sequence but all at once, each step
        }
        mr.material.color = endColor;
    }
}
