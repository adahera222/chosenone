using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartBattle : MonoBehaviour {

    Transform _transform;
    Transform _cameraTransform;

    void Awake()
    {
        _transform = transform;
        _cameraTransform = Camera.main.transform;
    }

    void Update() {
        if (_cameraTransform.position.x > _transform.position.x)
        {
            if (GameMaster.Instance.state == GameMaster.GameState.Playing && GameMaster.Instance.mode == GameMaster.GameMode.Walking)
            {
                Destroy(this);
                GameMaster.Instance.StartBattle(gameObject.name);
            }
        }
    }

}