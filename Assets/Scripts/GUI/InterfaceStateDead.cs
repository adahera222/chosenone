using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class InterfaceStateDead : IState
{
    public void Enter()
    {
        //GameMaster.Instance.interfaceManager.sceneFader.FadeOutFailure();
    }

    /// <summary>
    /// show example menu
    /// </summary>
    public void Update()
    {
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
            GameMaster.Instance.Resume();
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
