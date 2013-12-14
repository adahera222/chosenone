using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceManager : MonoBehaviour {

    public GUISkin skin;

    public Texture2D menuBackgroundTexture;

    public static float margin = 50.0f; // margin from screen border

    public static float buttonWidth = 260.0f;
    public static float buttonHeight = 80.0f;

    public SceneFader sceneFader;
    public StateMachine stateMachine;

    void Awake()
    {
        // create state machine
        stateMachine = new StateMachine();
        stateMachine.pushState(new InterfaceStateInGame());

        // get references to components
        sceneFader = gameObject.GetComponent<SceneFader>();
    }

    void Start()
    {
        GameMaster.Instance.gameStateChanged += Instance_gameStateChanged;
    }

    void Instance_gameStateChanged(GameMaster.GameState newState)
    {
        switch (newState)
        {
            case GameMaster.GameState.Menu:
                stateMachine.switchToState(new InterfaceStateMenu());
                break;
            case GameMaster.GameState.Playing:
                stateMachine.switchToState(new InterfaceStateInGame());
                break;
            case GameMaster.GameState.Paused:
                stateMachine.switchToState(new InterfaceStatePaused());
                break;
            case GameMaster.GameState.Dying:
                break;
            case GameMaster.GameState.Dead:
                break;
            case GameMaster.GameState.Loading:
                break;
            case GameMaster.GameState.Tutorial:
                stateMachine.switchToState(new InterfaceStateTutorial());
                break;
            default:
                break;
        }
    }

    void OnGUI()
    {
        if (GameMaster.Instance.state == GameMaster.GameState.Paused)
        {
            DrawMenuBackground();
        }

        stateMachine.Update();
    }

    private void DrawMenuBackground()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menuBackgroundTexture);
    }

}