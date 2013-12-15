using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlyDestroyOnLevel : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start() {

    }

    void Update() {

    }

    void OnLevelWasLoaded(int levelIndex)
    {
        if (levelIndex == 2)
        {
            Destroy(gameObject);
        }
    }

}