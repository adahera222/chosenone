using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorController : MonoBehaviour {

    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    const float OFFSET_X = 0.7f;
    const float MELEE_RANGE_SQUARED = 0.2f;

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
        GetMovementTarget();
    }

    void Update() {
        if (actor != null)
        {
            actor.Update();
        }

        if (actor.faction != Actor.Faction.Player)
        {
            MoveToTarget();
        }

        if (actor.state == Actor.ActionState.Dead)
        {
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }

        UpdateDepth();
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

    private void MoveToTarget()
    {
        Vector3 targetPosition;

        if (target != null)
        {
            // move
            if (_transform.position.x > target.transform.position.x) // to the right
            {
                targetPosition = new Vector3(target.transform.position.x + OFFSET_X, target.transform.position.y, _transform.position.z);
            }
            else // to the left
            {
                targetPosition = new Vector3(target.transform.position.x - OFFSET_X, target.transform.position.y, _transform.position.z);
            }

            Debug.DrawLine(_transform.position, targetPosition);

            Vector3 moveDirection = targetPosition - _transform.position;

            if (moveDirection.sqrMagnitude > MELEE_RANGE_SQUARED)
            {
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

    private void UpdateDepth()
    {
        _transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.y);
    }

    private void GetMovementTarget()
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