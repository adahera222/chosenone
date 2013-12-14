using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugInput : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Whaaa");
                GameMaster.Instance.RestartLastBattle();
            }
        }
    }

}