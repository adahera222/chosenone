using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class InterfaceStateMenu : IState
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
              (1 * Screen.height / 3) - (buttonHeight / 2),
              buttonWidth,
              buttonHeight
            ),
            "Start Game"
          )
        )
        {
            GameMaster.Instance.LoadTutorial();
        }


        if (!Application.isWebPlayer)
        {
            if (
  GUI.Button(
                // Center in X, 2/3 of the height in Y
    new Rect(
      Screen.width / 2 - (buttonWidth / 2),
      (2 * Screen.height / 3) - (buttonHeight / 2),
      buttonWidth,
      buttonHeight
    ),
    "Exit"
  )
)
            {
                // Reload the level
                GameMaster.Instance.Quit();
            }
        }


    }

    public void Exit()
    {

    }
}
