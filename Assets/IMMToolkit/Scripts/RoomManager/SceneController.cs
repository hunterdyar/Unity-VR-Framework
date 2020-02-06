using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyBox;
using UnityEngine.Events;

//THis script goes in a "Master" scene, that scene is always loaded. It should have very few objects.
//Probably just this and the XR rig, maybe some background music or skybox things, managers/systems/singletons, etc.

//It will additively load just one other scene, and allow you to cycle through them.

//Setup the list of the scenes and the starting Index, then use NextScene() and PreviousScene() to do the transitions.

//TODO is probably implement a delay.
public class SceneController : MonoBehaviour
{
    //Scene references are not built into Unity, this is a property of MyBox.
    public List<SceneReference> scenes = new List<SceneReference>();
    [Space]
    public int startingSceneIndex;
    [ReadOnly]
    [SerializeField]
    private int currentSceneIndex;
    public UnityEvent onSceneLoaded;
    void Start()
    {
        if(SceneManager.sceneCount > 1){
            //More than one scene is loaded. We are probably testing things in the inspector or some unique runtime setup, lets not do initial scene populate 
            return;
        }else
        {
            GoToScene(startingSceneIndex);
        }
    }
    [ButtonMethod]
    public void NextScene(){
        if(currentSceneIndex == scenes.Count-1)
        {
            
            GoToScene(0);
        }else{
            GoToScene(currentSceneIndex+1);
        }
    }
    [ButtonMethod]
    public void PreviousScene()
    {
        if(currentSceneIndex == 0)
        {
            GoToScene(scenes.Count-1);
        }else{
            GoToScene(currentSceneIndex-1);
        }
    }
    [ButtonMethod]
    public void UnloadAll()//..except this one
    {
        for(int i = 0;i<SceneManager.sceneCount;i++)
        {
            Scene s = SceneManager.GetSceneAt(i);

            if(s.name != gameObject.scene.name)//don't unload the master scene (that this script should be in), just all other ones.
            {
                SceneManager.UnloadSceneAsync(s.name);
            }
        }
    }
    public void GoToScene(int indexToLoad)
    {
        StartCoroutine(LoadScene(indexToLoad));

    }
    IEnumerator LoadScene(int indexToLoad)
    {
        if(SceneManager.GetSceneByName(scenes[currentSceneIndex].SceneName).isLoaded)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(scenes[currentSceneIndex].SceneName);
            while(!unload.isDone){
                //loop waiting until scene done loading.
                yield return null;
            }
        }
        AsyncOperation load = SceneManager.LoadSceneAsync(scenes[indexToLoad].SceneName,LoadSceneMode.Additive);
        while(!load.isDone){
            yield return null;
        }
        //Scene done loading!
        currentSceneIndex = indexToLoad;
        onSceneLoaded.Invoke();
    }
    
}
