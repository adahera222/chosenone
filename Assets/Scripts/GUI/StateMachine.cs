using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine
{

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private Stack<IState> stack;

    // ================================================================================
    //  constructor
    // --------------------------------------------------------------------------------

    public StateMachine()
    {
        stack = new Stack<IState>();
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void pushState(IState state)
    {
        stack.Push(state);
        state.Enter();
    }

    public void switchToState(IState state)
    {
        popAll();
        pushState(state);
    }

    public IState popState()
    {
        if (stack.Count > 0)
        {
            IState state = stack.Pop();
            state.Exit();
            return state;
        }
        else
        {
            return null;
        }
    }

    public void popAll()
    {
        while (stack.Count > 0)
        {
            popState();
        }
    }

    internal void Update()
    {
        if (stack.Count > 0)
        {
            stack.Peek().Update();
        }
    }
}