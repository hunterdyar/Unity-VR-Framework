using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
//Allows the ambient scene lighting to be a member of a lighting group, and fade with them.

namespace IMMToolkit {
    public class AmbientLightgroupmember : LightgroupMember
    {
        void Awake(){
            if(allCurrentlyLoadedLights == null)
            {
                allCurrentlyLoadedLights = ScriptableObject.CreateInstance<LightingGroup>();
            }
            defaultIntensity = RenderSettings.ambientIntensity;
            if(defaultIntensity == 0)
            {
                Debug.LogWarning("ambient default is 0. Is this intentional?");
            }
        }
        //RenderSettings.AmbientLight
        public override void FadeOut(float timeToFade)
        {
            
            if(RenderSettings.ambientIntensity != 0)
            {
                StartCoroutine(FadeLight(RenderSettings.ambientIntensity,0,timeToFade));
            }
        }
        public override void FadeIn(float timeToFade)
        {
            if(RenderSettings.ambientIntensity != defaultIntensity)
            {
                Debug.Log("fade out ambient");
                StartCoroutine(FadeLight(RenderSettings.ambientIntensity,defaultIntensity,timeToFade));
            }
        }
        public override IEnumerator FadeLight(float startIntensity, float endIntensity,float timeToFade)
        {
            float t = 0;
            while(t<1)
            {
                t = t + Time.deltaTime/timeToFade;
                RenderSettings.ambientIntensity = Mathf.Lerp(startIntensity,endIntensity,t);
                yield return new WaitForEndOfFrame();
            }
            RenderSettings.ambientIntensity = endIntensity;
        }
    }

}