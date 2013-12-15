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

    FadingTimer messageTimer = null;
    string message = "";

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
        OnSwitchedState(GameMaster.Instance.state);
    }

    void Instance_gameStateChanged(GameMaster.GameState newState)
    {
        OnSwitchedState(newState);
    }

    private void OnSwitchedState(GameMaster.GameState newState)
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
                stateMachine.switchToState(new InterfaceStateDead());
                break;
            case GameMaster.GameState.Loading:
                break;
            case GameMaster.GameState.Outro:
                stateMachine.switchToState(new InterfaceStateOutro());
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

        if (skin != null)
        {
            GUI.skin = skin;
        }

        UpdateMessages();

        if (!Application.isLoadingLevel)
        {
            stateMachine.Update();            
        }
    }

    public void DrawMenuBackground()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menuBackgroundTexture);
    }

    public void ShowMessage(string message)
    {
        messageTimer = new FadingTimer(0.5f, 3.0f, 0.5f);
        this.message = message;
    }

    private void UpdateMessages()
    {
        if (messageTimer != null)
        {
            messageTimer.Update();
            float progress = messageTimer.progress;

            GUI.color = new Color(1.0f, 1.0f, 1.0f, progress);

            Rect rect = new Rect(Screen.width / 2.0f - buttonWidth / 2.0f, Screen.height / 2.0f - buttonHeight * 2.5f, buttonWidth, buttonHeight);
            GUI.Label(rect, message);

            GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            if (messageTimer.hasEnded)
            {
                messageTimer = null;
            }
        }
    }
}