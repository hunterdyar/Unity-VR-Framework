using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMMToolkit{
    [RequireComponent(typeof(Light))]
    public class LightgroupMember : MonoBehaviour
    {
        public LightingGroup lightingGroup;

        [HideInInspector]
        public new Light light;
        private float defaultIntensity;//the "on"
        void Awake(){
            light = GetComponent<Light>();
            defaultIntensity = light.intensity;
        }
        void OnEnable(){
            lightingGroup.RegisterMember(this);
        }
        void OnDisable(){
            lightingGroup.RegisterMember(this);
        }
        public void FadeOut(float timeToFade)
        {
            StartCoroutine(FadeLight(light.intensity,0,timeToFade));
        }
        public void FadeIn(float timeToFade)
        {
            StartCoroutine(FadeLight(light.intensity,defaultIntensity,timeToFade));
        }
        public IEnumerator FadeLight(float startIntensity, float endIntensity,float timeToFade)
        {
            float t = 0;
            while(t<1)
            {
                t = t + Time.deltaTime/timeToFade;
                light.intensity = Mathf.Lerp(startIntensity,endIntensity,t);
                yield return new WaitForEndOfFrame();
            }
            light.intensity = endIntensity;
        }
        
    }
}
