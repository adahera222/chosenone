using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void Start()
    {
        GetComponent<ActorController>().actor = Actor.GetPlayer();
        GameMaster.Instance.player = GetComponent<ActorController>();
    }

    void Update() {

    }

    void OnDestroy()
    {
        if (GameMaster.Instance.player == GetComponent<ActorController>())
        {
            GameMaster.Instance.player = null;
        }
    }

}