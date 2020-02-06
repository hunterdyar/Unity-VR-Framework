using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public class GameObjectSwitcherHelper : MonoBehaviour
{
    public GameObjectSwitcher roomSwitcher;
    void Start()
    {
        FindSwitcher();
    }
    void FindSwitcher()
    {
        roomSwitcher = GameObject.FindObjectOfType<GameObjectSwitcher>();
    }

    [ButtonMethod]
    public void SetThisRoom()
    {
        roomSwitcher = GameObject.FindObjectOfType<GameObjectSwitcher>();
        //add me to the list of scene objects if I am not already there.
        if(!roomSwitcher.gameObjects.Contains(gameObject))
        {
            roomSwitcher.gameObjects.Add(gameObject);
        }
        roomSwitcher.SetOne(gameObject,true);
    }
}
