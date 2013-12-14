using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GameMaster : MonoBehaviour
{
    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        Dying,
        Dead,
        Loading
    }

    public const float edgeTop = 1.0f;
    public const float edgeBottom = -2.7f;

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Dictionary<string,Battle> battles = new Dictionary<string,Battle>();
    public Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    public static GameMaster Instance = null;

    public GameState state = GameState.Playing;

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

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateBattles();

        gameObject.SendMessage("OnLevelWasLoaded",Application.loadedLevel);
    }

    void Start()
    {
        
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

        float horizontalDistance = Camera.main.orthographicSize * Camera.main.aspect;

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
}
