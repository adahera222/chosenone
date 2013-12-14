using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class InterfaceStateDead : IState
{
    bool started = false;

    Timer startTimer = null;
    FadingTimer timer = null;

    public void Enter()
    {
        startTimer = new Timer(2.0f);
    }

    /// <summary>
    /// show example menu
    /// </summary>
    public void Update()
    {
        if (startTimer != null)
        {
            startTimer.Update();

            if (!startTimer.HasFinished())
            {
                return;
            }

            startTimer = null;
            timer = new FadingTimer(0.5f, 1.0f);
        }

        timer.Update();

        float progress = timer.progress;
        GUI.color = new Color(1.0f, 1.0f, 1.0f, progress);

        GameMaster.Instance.interfaceManager.DrawMenuBackground();

        const int buttonWidth = 260;
        const int buttonHeight = 80;

        if (
          GUI.Button(
            // Center in X, 1/3 of the height in Y
            new Rect(
              Screen.width / 2 - (buttonWidth / 2),
              (1 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Restart"
          )
        )
        {
            GameMaster.Instance.RestartLastBattle();
        }

        if (
          GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(
              Screen.width / 2 - (buttonWidth / 2),
              (2 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Back to Menu"
          )
        )
        {
            // Reload the level
            GameMaster.Instance.StartMenu();
        }
    }

    public void Exit()
    {

    }
}
