using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraParallax : MonoBehaviour {

    Transform backgroundTransform;
    Transform middleGroundTransform;

    void Start() {
        backgroundTransform = GameObject.Find("0 - Background").transform;
        middleGroundTransform = GameObject.Find("1 - Middleground").transform;
    }

    void Update() {

    }

    public void Move(Vector3 moveDelta)
    {
        transform.Translate(moveDelta);

        if (backgroundTransform != null)
        {
            backgroundTransform.Translate(moveDelta * 0.5f);
        }

        if (middleGroundTransform != null)
        {
            middleGroundTransform.Translate(moveDelta * 0.25f);
        }
    }
}