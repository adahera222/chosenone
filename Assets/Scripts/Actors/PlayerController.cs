using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    const float movementMargin = 0.5f;

    private ActorController actorController;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void Start()
    {
        actorController = GetComponent<ActorController>();

        actorController.actor = Actor.GetPlayer();
        GameMaster.Instance.player = GetComponent<ActorController>();
    }

    void Update() {

        /*
         *  movement
         */

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // check directions
        if (inputX > 0)
            actorController.SetMoveDirection(true);
        if (inputX < 0)
            actorController.SetMoveDirection(false);

        // movement
        Vector3 movement = new Vector3(actorController.speed.x * inputX, actorController.speed.y * inputY, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        /*
         *  checking 'out of bounds'
         */

        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x + movementMargin;

        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x - movementMargin;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, GameMaster.edgeBottom, GameMaster.edgeTop),
          transform.position.z
        );
    }

    void OnDestroy()
    {
        if (GameMaster.Instance.player == GetComponent<ActorController>())
        {
            GameMaster.Instance.player = null;
        }
    }

}