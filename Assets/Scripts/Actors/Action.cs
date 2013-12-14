using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action {

    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public enum ActionType
    {
        MeleeAttack,
        Wait
    }

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public ActorController source;
    public ActorController target;

    public ActionType type;
    public float delta = 1.0f;
    public float duration = 1.0f;
    public bool canMove = true;

    public bool hasStarted = false;

    public Action followWithAction = null;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private Timer timer = new Timer();

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void StartAction()
    {
        //Debug.Log("Action started: " + Time.time);

        hasStarted = true;
        timer.duration = duration;
        timer.Reset();
    }

    public void Update()
    {
        if (hasStarted)
        {
            timer.Update();

            if (timer.HasFinished())
            {
                hasStarted = false;
                ExecuteAction();
            }
        }
    }

    public void EndAction()
    {
        hasStarted = false;

        //Debug.Log("Action ended: " + Time.time);
    }

    public bool HasFinished()
    {
        return timer.HasFinished();
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void ExecuteAction()
    {
        switch (type)
        {
            case ActionType.MeleeAttack:
                float d = source.actor.DealDamage(this);
                target = source.focusManager.focus;
                if (target != null)
                {
                    target.actor.ApplyDamage(d);
                }
                break;
            case ActionType.Wait:
                break;
            default:
                break;
        }
    }

    public static Action GetWaitAction(float time)
    {
        Action action = new Action();
        action.type = ActionType.Wait;
        action.duration = time;

        return action;
    }

    public static Action GetMeleeAction(float delta, float time, float waitAfter = 0.5f)
    {
        Action action = new Action();
        action.type = ActionType.MeleeAttack;
        action.duration = time;
        action.delta = delta;

        if (waitAfter > 0)
        {
            action.followWithAction = GetWaitAction(waitAfter);
        }

        return action;
    }
}