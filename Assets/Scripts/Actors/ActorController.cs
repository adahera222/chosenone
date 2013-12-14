﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorController : MonoBehaviour {

    const float offsetX = 0.7f;

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Actor actor { get; set; }

    public ActorController target = null;
    public GameObject displayObject;

    public bool directionRight = true;

    public FocusManager focusManager = null;

    public Vector2 speed = new Vector2(4.0f, 3.0f);

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private FocusManager focusRight = null;
    private FocusManager focusLeft = null;
    private Transform _transform;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void Awake()
    {
        _transform = transform;

        focusRight = transform.Find("focusRight").GetComponent<FocusManager>();
        focusLeft = transform.Find("focusLeft").GetComponent<FocusManager>();

        UpdateFocusDirection();
    }

    void Start() {
        GetTarget();
    }

    void Update() {
        if (actor.faction != Actor.Faction.Player)
        {
            TakeAction();
        }
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void SetMoveDirection(bool toTheRight)
    {
        if (toTheRight != directionRight)
        {
            flipDirection();
        }
    }

    public void flipDirection()
    {
        directionRight = !directionRight;

        // change visual avatar
        Transform t = displayObject.transform;
        t.localScale = new Vector3(t.localScale.x * -1.0f, t.localScale.y, t.localScale.z);

        UpdateFocusDirection();
    }

    public void SetActor(Actor actor)
    {
        this.actor = actor;
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void TakeAction()
    {
        Vector3 targetPosition;

        if (target != null)
        {
            if (focusManager.focus == target)
            {
                // attack
            }
            else
            {
                // move
                if (_transform.position.x > target.transform.position.x) // to the right
                {
                    targetPosition = new Vector3(target.transform.position.x + offsetX, target.transform.position.y, 0);
                }
                else // to the left
                {
                    targetPosition = new Vector3(target.transform.position.x - offsetX, target.transform.position.y, 0);
                }

                Debug.DrawLine(_transform.position, targetPosition);

                Vector3 moveDirection = targetPosition - _transform.position;
                moveDirection.Normalize();

                Vector3 movement = new Vector3(speed.x * moveDirection.x, speed.y * moveDirection.y, 0);
                movement *= Time.deltaTime;
                transform.Translate(movement);

                if (movement.x > 0)
                    SetMoveDirection(true);
                if (movement.x < 0)
                    SetMoveDirection(false);
            }
        }
    }

    private void GetTarget()
    {
        if (actor.faction != Actor.Faction.Player)
        {
            target = GameMaster.Instance.player;
        }
    }

    private void UpdateFocusDirection()
    {
        if (directionRight)
        {
            focusManager = focusRight;
        }
        else
        {
            focusManager = focusLeft;
        }
    }

}