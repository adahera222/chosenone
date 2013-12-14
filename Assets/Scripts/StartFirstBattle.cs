using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartFirstBattle : MonoBehaviour {

    void Start() {
        GameMaster.Instance.StartFirstBattle();
    }
}