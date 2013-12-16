using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlyDestroyOnLevel : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int levelIndex)
    {
        if (levelIndex == 2)
        {
            Destroy(gameObject);
        }
    }

}