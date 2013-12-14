using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorController : MonoBehaviour {

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Actor actor { get; set; }

    public ActorController target = null;
    public GameObject displayObject;

    public bool directionRight = true;

    public FocusManager focus = null;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private FocusManager focusRight = null;
    private FocusManager focusLeft = null;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    void Awake()
    {
        focusRight = transform.Find("focusRight").GetComponent<FocusManager>();
        focusLeft = transform.Find("focusLeft").GetComponent<FocusManager>();

        UpdateFocusDirection();
    }

    void Start() {
        GetTarget();
    }

    void Update() {

    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void flipDirection()
    {
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
            focus = focusRight;
        }
        else
        {
            focus = focusLeft;
        }
    }

}