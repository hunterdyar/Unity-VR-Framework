using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IMMToolkit{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceInterface : MonoBehaviour
    {
        [MyBox.Separator("Setup")]
        [Tooltip("The audio group will stop all other members of the group from playing audio, so you don't have overlapping sound from members of a group.")]
        public AudioGroup[] groups;

        [MyBox.Separator("Debugging")]
        [MyBox.ReadOnly]
        public AudioSource audioSource;
        private AudioClip defaultClip;
        private float defaultVolume;
        
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            defaultClip = audioSource.clip;
            defaultVolume = audioSource.volume;
        }
        [MyBox.ButtonMethod]
        public void Play()
        {
            if(groups.Length != 0)
            {
                foreach(AudioGroup g in groups){
                    //This has the interesting "bug" of telling the audio source to play multiple times in one frame, 
                    //This is fine, we can stop all members of all other audio groups.
                    //an audio source can only play one clip at a time, and since its the same frame, there shouldn't be any stuttering, 
                    groups[0].PlayOne(this);
                }
            }else
            {
                audioSource.Play();
            }
        }
        public void Stop()
        {
            audioSource.Stop();
        }
        public void ForcePlay()//ignores the group
        {
            audioSource.Play();
        }
        public void SetClip(AudioClip clip)
        {
            audioSource.clip = clip;
        }
        public AudioClip GetClip()
        {
            return audioSource.clip;
        }
        public float GetVolume()
        {
            return audioSource.volume;
        }
        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
        public void FadeVolumeByFactor(float fadeFactor)
        {
            audioSource.volume = defaultVolume*fadeFactor;
        }
        public IEnumerator FadeVolumeByFactor(float fadeFactor,float timeToFade,float waitTillFade = 0)
        {
            yield return new WaitForSeconds(waitTillFade);
            float t = 0;
            float startVolume = audioSource.volume;
            float endVolume = defaultVolume*fadeFactor;
            while(t<1)
            {
                t = t + Time.deltaTime/timeToFade;
                audioSource.volume = Mathf.Lerp(startVolume,endVolume,t);
                yield return new WaitForEndOfFrame();
            }
            audioSource.volume = endVolume;
        }
        public void StopAllMembers()
        {
            foreach(AudioGroup g in groups)
            {
                g.StopAll();
            }
        }
        public void ResetVolume()
        {
            audioSource.volume = defaultVolume;
        }
        public void ResetClip()
        {
            audioSource.clip = defaultClip;
        }
        void OnEnable()
        { 
            foreach(AudioGroup g in groups)
            {
                g.RegisterMember(this);
            }
        }
        void OnDisable()
        {
            foreach(AudioGroup g in groups)
            {
                g.DeregisterMember(this);
            } 
        }
    }
}
