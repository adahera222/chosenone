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

    public ActorController self;
    public ActorController target;

    public ActionType type;
    public float delta = 1.0f;
    public float duration = 1.0f;
    public bool canMove = false;

    public bool hasStarted = false;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private Timer timer = new Timer();

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void StartAction()
    {
        hasStarted = true;
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

    public static Action GetMeleeAction(float delta, float time)
    {
        Action action = new Action();
        action.type = ActionType.MeleeAttack;
        action.duration = time;
        action.delta = delta;

        return action;
    }
}