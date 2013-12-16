using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlyDestroyOnStart : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int levelIndex)
    {
        if (levelIndex == 0)
        {
            Destroy(gameObject);
        }
    }
}