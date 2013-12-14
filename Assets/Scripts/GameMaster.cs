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

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Dictionary<string,Battle> battles = new Dictionary<string,Battle>();
    public Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();

    public static GameMaster Instance = null;

    public GameState state = GameState.Playing;

    public ActorController player = null;

    // ================================================================================
    //  Unity methods
    // --------------------------------------------------------------------------------

    public void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateBattles();
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void CreateBattles()
    {
        battles["test"] = new Battle();
        battles["test"].enemies.Add(new Actor() {
            maxHealth = 10.0f, weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword)
        });
        battles["test"].enemies.Add(new Actor()
        {
            maxHealth = 10.0f, weapon = Weapon.GetWeapon(Weapon.WeaponType.Axe)
        });

    }


}
