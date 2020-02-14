using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMMToolkit{
    public class LightgroupMember : MonoBehaviour
    {   
        public bool initiateAtZero;
        public LightingGroup[] lightingGroups;
        public static LightingGroup allCurrentlyLoadedLights;
        [HideInInspector]
        public new Light light;
        [HideInInspector]
        public float defaultIntensity;//the "on"
        void Awake(){
            //needs to potentially create this lighting group before OnEnable, where it will use it.
            if(allCurrentlyLoadedLights == null)
            {
                allCurrentlyLoadedLights = ScriptableObject.CreateInstance<LightingGroup>();
            }
            //
            light = GetComponent<Light>();
            if(light == null){
                Debug.LogError("no light component found for lightGroupMember",gameObject);
            }
            defaultIntensity = light.intensity;
            
            if(initiateAtZero)
            {
                light.intensity = 0;
            }
            
        }
        void OnEnable(){
            allCurrentlyLoadedLights.RegisterMember(this);
            foreach(LightingGroup lightingGroup in lightingGroups)
            {
                lightingGroup.RegisterMember(this);
            }
        }
        void OnDisable(){
            allCurrentlyLoadedLights.DeregisterMember(this);
            foreach(LightingGroup lightingGroup in lightingGroups)
            {
                lightingGroup.DeregisterMember(this);
            }
        }
        public virtual void FadeOut(float timeToFade)
        {
            if(light.intensity != 0)//prevent uneccesary coroutines
            {
                StartCoroutine(FadeLight(light.intensity,0,timeToFade));
            }
        }
        public virtual void FadeIn(float timeToFade)
        {
            if(light.intensity != defaultIntensity)//nothing would happen anyway
            {
                StartCoroutine(FadeLight(light.intensity,defaultIntensity,timeToFade));
            }
        }
        public virtual IEnumerator FadeLight(float startIntensity, float endIntensity,float timeToFade)
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
        ///DEBUGGING
        [MyBox.ButtonMethod]
        public void TurnThisLightOn(){
           FadeIn(0.1f);
        }
        [MyBox.ButtonMethod]
        public void TurnThisLightOff(){
            FadeOut(0.1f);
        }
        [MyBox.ButtonMethod]
        public void FadeAllIn(){
            allCurrentlyLoadedLights.FadeLightsIn();
        }
        [MyBox.ButtonMethod]
        public void FadeAllOut(){
            allCurrentlyLoadedLights.FadeLightsOut();
        }
    }
}
