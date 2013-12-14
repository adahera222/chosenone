using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FocusManager : MonoBehaviour {

    private List<ActorController> actors = new List<ActorController>();

    public ActorController focus = null;

    void Start() {

    }

    void Update() {

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

        if (otherActor != null && actors.Contains(otherActor))
        {
            actors.Remove(otherActor);

            if (focus == otherActor)
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