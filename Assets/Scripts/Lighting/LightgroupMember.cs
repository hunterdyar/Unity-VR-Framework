using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMMToolkit{
    [RequireComponent(typeof(Light))]
    public class LightgroupMember : MonoBehaviour
    {
        [HideInInspector]
        public new Light light;
        public LightingGroup lightingGroup;
        
        void Awake(){
            light = GetComponent<Light>();
        }
        void OnEnable(){
            lightingGroup.RegisterMember(light);
        }
        void OnDisable(){
            lightingGroup.RegisterMember(light);
        }
    }
}
