using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FocusManager : MonoBehaviour {

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public ActorController focus = null;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private List<ActorController> actors = new List<ActorController>();

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------
    
    void Update()
    {
        if (focus != null && focus.actor.state == Actor.ActionState.Dead)
        {
            RemoveFromList(focus);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ActorController otherActor = other.gameObject.GetComponent<ActorController>();

        if (otherActor != null && !actors.Contains(otherActor))
        {
            if (focus == null)
            {
                focus = otherActor;
            }

            actors.Add(otherActor);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ActorController otherActor = other.gameObject.GetComponent<ActorController>();

        RemoveFromList(otherActor);
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void RemoveFromList(ActorController actor)
    {
        if (actor != null && actors.Contains(actor))
        {
            actors.Remove(actor);

            if (focus == actor)
            {
                focus = null;

                if (actors.Count > 0)
                {
                    focus = actors[0];
                }
            }
        }
    }

}