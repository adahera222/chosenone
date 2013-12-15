using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class InterfaceStateOutro : IState
{
    public void Enter()
    {

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
      Screen.height / 2 + buttonHeight *  2.0f,
      buttonWidth,
      buttonHeight
    ),
            "Return To Menu"
          )
        )
        {
            GameMaster.Instance.StartMenu();
        }
    }

    public void Exit()
    {

    }
}
