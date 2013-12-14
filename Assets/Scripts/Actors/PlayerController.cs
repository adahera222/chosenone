using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    void Awake()
    {
        GetComponent<ActorController>().actor.SetToPlayer();
        GameMaster.Instance.player = GetComponent<ActorController>();
    }

    void Start() {

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