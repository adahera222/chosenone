using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FadeInAtStart : MonoBehaviour {

    void Start() {
        GameMaster.Instance.interfaceManager.sceneFader.FadeIn();
    }

}