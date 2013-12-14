using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GameMaster : MonoBehaviour
{
    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public delegate void GameStateChangeHandler(GameState newState);

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        Dying,
        Dead,
        Loading,
        Tutorial
    }

    public const float edgeTop = 1.0f;
    public const float edgeBottom = -2.7f;

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public event GameStateChangeHandler gameStateChanged;

    public Dictionary<string,Battle> battles = new Dictionary<string,Battle>();
    public Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    public static GameMaster Instance = null; // Singleton

    public InterfaceManager interfaceManager;

    private GameState _state;
    public GameState state
    {
        get { return _state; }
        set
        {
            _state = value;
            if (gameStateChanged != null)
            {
                gameStateChanged(value);
            }
        }
    }

    public ActorController player = null;
    public List<ActorController> actors = new List<ActorController>();

    public GameObject actorPrefab;

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private List<Vector3> randomPositions;
    private string lastBattleName;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    public void Awake()
    {
        if (Instance == this)
        {
            return;
        }

        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _state = GameState.Playing;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        interfaceManager = GetComponent<InterfaceManager>();

        CreateBattles();

        gameObject.SendMessage("OnLevelWasLoaded",Application.loadedLevel);
    }

    void Update()
    {
        // special keys
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Playing)
            {
                Pause();
            } else if (state == GameState.Paused)
            {
                Resume();
            }            
        }
    }

    void OnLevelWasLoaded(int level)
    {
        StartCoroutine(StartTestBattle());
    }

    private IEnumerator StartTestBattle()
    {
        yield return new WaitForSeconds(0.1f);
        StartBattle("test");
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void StartBattle(string battleName)
    {
        Battle battle = battles[battleName];

        if (battle == null)
        {
            Debug.Log("Battle " + battleName + " not found");
            return;
        }

        CreateRandomPositions();
        foreach (var enemy in battle.enemies)
        {
            SpawnEnemy(enemy);
        }

        lastBattleName = battleName;
    }

    public void RestartLastBattle()
    {
        DestroyActors();

        StartBattle(lastBattleName);
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void DestroyActors()
    {
        for (int i = 0; i < actors.Count; i++)
        {
            Destroy(actors[i].gameObject);
        }

        actors.Clear();
    }

    private void SpawnEnemy(Actor enemy)
    {
        // create object
        GameObject newObject = (GameObject)Instantiate(actorPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // set to parent
        Transform parentObject = GameObject.Find("3 - Actors").transform;
        newObject.transform.parent = parentObject;

        // set values
        ActorController actorController = newObject.GetComponent<ActorController>();
        actorController.SetActor(enemy);

        // add to list
        actors.Add(actorController);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int i = Random.Range(0, randomPositions.Count);
        Vector3 pos = randomPositions[i];
        randomPositions.RemoveAt(i);
        return pos;
    }

    private void CreateRandomPositions()
    {
        randomPositions = new List<Vector3>();

        Rect bounds = Utilities2D.CameraBounds2D();

        float distanceFromEdge = 2.0f;

        for (float i = 0; i <= 1.0f; i += 0.2f)
        {
            float y = Mathf.Lerp(edgeBottom, edgeTop, i);

            randomPositions.Add(new Vector3(bounds.xMin - distanceFromEdge, y, 0));
            randomPositions.Add(new Vector3(bounds.xMax + distanceFromEdge, y, 0));
        }
    }

    private void CreateBattles()
    {
        battles["test"] = new Battle();
        for (int i = 0; i < 8; i++)
        {
            battles["test"].enemies.Add(Actor.GetMob());
        }

        battles["beginning"] = new Battle();
        for (int i = 0; i < 2; i++)
        {
            battles["beginning"].enemies.Add(Actor.GetMob());
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        state = GameState.Paused;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        state = GameState.Playing;
    }

    public void StartMenu()
    {
        Application.LoadLevel(0);
        state = GameState.Menu;
    }

    public void StartGame()
    {
        Application.LoadLevel(2);
        state = GameState.Playing;
    }

    public void LoadTutorial()
    {
        Application.LoadLevel(1);
        state = GameState.Tutorial;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
