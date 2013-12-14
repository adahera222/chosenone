using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simpler Timer, which has to be updated from outside
/// </summary>
public class Timer
{

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public float duration = 3.0f;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private float _elapsedTime = 0;

    // ================================================================================
    //  constructor
    // --------------------------------------------------------------------------------

    public Timer()
    {
        // not needed
    }

    public Timer(float duration)
    {
        this.duration = duration;
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    public bool HasFinished()
    {
        return _elapsedTime >= duration;
    }

    public void Reset()
    {
        _elapsedTime = 0;
    }

    public void SetRandomStart(float progress = 1.0f)
    {
        _elapsedTime = Random.Range(0, duration * progress);
    }
}