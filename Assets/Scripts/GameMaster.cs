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
        Tutorial,
        Outro
    }

    public enum GameMode
    {
        Battle,
        Walking
    }

    public const float edgeTop = 0.2f;
    public const float edgeBottom = -3.1f;

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

    public GameMode mode = GameMode.Walking;

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

        if (Application.loadedLevel == 0)
        {
            _state = GameState.Menu;
        }
        else if (Application.loadedLevel == 1)
        {
            _state = GameState.Tutorial;
        }
        else if (Application.loadedLevel == Application.levelCount - 1)
        {
            _state = GameState.Outro;
        }
        else
        {
            _state = GameState.Playing;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        interfaceManager = GetComponent<InterfaceManager>();

        CreateBattles();
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
        mode = GameMode.Walking;
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

        mode = GameMode.Battle;
        GameMaster.Instance.interfaceManager.ShowMessage("Fight!");
    }

    public void RestartLastBattle()
    {
        DestroyActors();

        RevivePlayer();
        state = GameState.Playing;

        StartBattle(lastBattleName);
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
        DestroyActors();
        Time.timeScale = 1.0f;
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

    public void StartFirstBattle()
    {
        StartBattle("test");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayerDies()
    {
        state = GameState.Dead;
    }

    public void EnemyDies()
    {
        foreach (var actor in actors)
        {
            if (actor.actor.state != Actor.ActionState.Dead)
            {
                return;
            }
        }

        // no one left
        WinBattle();
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void WinBattle()
    {
        mode = GameMode.Walking;
        player.actor.SetToFullHealth();
        GameMaster.Instance.interfaceManager.ShowMessage("Battle Won");
        StartCoroutine(ClearBattleField());
    }

    private IEnumerator ClearBattleField()
    {
        yield return new WaitForSeconds(2.0f);
        DestroyActors();
    }

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
        enemy.Revive();
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
        Battle battle;

        battle = new Battle();
        for (int i = 0; i < 1; i++)
        {
            battle.enemies.Add(Actor.GetMob());
        }
        battles["test"] = battle;

        battle = new Battle();
        for (int i = 0; i < 2; i++)
        {
            battle.enemies.Add(Actor.GetMob());
        }
        battles["beginning"] = battle;

        battle = new Battle();
        for (int i = 0; i < 4; i++)
        {
            battle.enemies.Add(Actor.GetMob());
        }
        battles["progress0"] = battle;


    }

    private void RevivePlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 2.0f, 0);
        player.GetComponent<ActorController>().enabled = true;
        player.GetComponent<Collider2D>().enabled = true;
        player.GetComponent<ActorController>().actor.Revive();
    }
}
