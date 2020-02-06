using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{
    [Header("Configuration")]
    public List<GameObject> gameObjects = new List<GameObject>();
    [Header("Settings")]
    public int startingGOIndex = 0;

    private int currentIndex = 0;
    public bool wrapRooms = true;
    void Start(){
        SetOne(startingGOIndex,true,true,0);
    }
    public void NextRoom(float delay = 0){
        currentIndex++;
        if(currentIndex >= gameObjects.Count)
        {
            if(wrapRooms){currentIndex = 0;}
        }else{
            Debug.LogWarning("Cant go to Next object, at last one");
            return;
        }
        SetOne(currentIndex,true,true,delay);
    }
    public void PreviousRoom(float delay = 0){
        currentIndex--;
        if(currentIndex < 0)
        {
            if(wrapRooms){currentIndex = gameObjects.Count-1;}
        }else{
            Debug.LogWarning("Cant go to previous go, at first gameObject");
            return;
        }
        SetOne(currentIndex,true,true,delay);
    }
    public void SetAll(bool active,float delay = 0)
    {
        if(delay == 0)
        {
            foreach(GameObject go in gameObjects)
            {
                go.SetActive(active);
            }
        }else if(delay > 0){
            StartCoroutine(SetAllDelayed(active,delay));
        }
    }
    IEnumerator SetAllDelayed(bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAll(active,0);//If this isn't 0, we will have a rather stupid infinite loop.
    }
    public void SetOne(int index, bool active, bool setOthersOpposite = true, float delay = 0)
    {
        if(index > 0 && index <gameObjects.Count)
        {
            SetOne(gameObjects[index],active,setOthersOpposite,delay);
        }else{
            Debug.LogError("provided gameObject index outside of bounds",this);
        }
    }
    public void SetOne(GameObject go,bool active, bool setOthersOpposite = true,float delay = 0)
    {
        if(delay == 0)
        {
            if(setOthersOpposite){
                SetAll(!active,0);//want to do this instantly.
            }
            go.SetActive(active);
        }else
        {
            StartCoroutine(SetOneDelayed(go,active,setOthersOpposite,delay));
        }
    }
    IEnumerator SetOneDelayed(GameObject go,bool active, bool setOthersOpposite,float delay)
    {
        yield return new WaitForSeconds(delay);
        SetOne(go,active,setOthersOpposite,0);
    }
}
