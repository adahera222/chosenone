using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor {

    // ================================================================================
    //  declarations
    // --------------------------------------------------------------------------------

    public enum Faction
    {
        Player,
        Enemy
    }

    public enum ActionState
    {
        Dead,
        Idle,
        TakingAction
    }

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Faction faction;

    //state
    public ActionState state;

    // stats
    private float _maxHealth;
    public float maxHealth
    {
        get { return _maxHealth; }
        set {
            _maxHealth = value;
            health = maxHealth;
        }
    }        
    public float health;
    public float damageModifier = 1.0f;
    public float defense = 1.0f;

    // inventory
    public Weapon weapon = null;

    // ================================================================================
    //  private
    // --------------------------------------------------------------------------------

    private List<Action> _actions = new List<Action>();

    // ================================================================================
    //  constructor
    // --------------------------------------------------------------------------------

    public Actor()
    {
        faction = Faction.Enemy;

        if (weapon == null)
        {
            weapon = new Weapon();
        }

        state = ActionState.Idle;
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public void Update()
    {
        UpdateActionList();
    }

    private void UpdateActionList()
    {
        if (_actions.Count == 0)
        {
            return;
        }

        Action currentAction = _actions[0];

        if (!currentAction.hasStarted)
        {
            currentAction.StartAction();
            return;
        }

        currentAction.Update();

        if (currentAction.HasFinished())
        {
            _actions.RemoveAt(0);
        }
    }

    // ================================================================================
    //  factory methods
    // --------------------------------------------------------------------------------

    public static Actor GetPlayer()
    {
        Actor actor = new Actor();

        actor.faction = Faction.Player;
        actor.weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword);
        actor.maxHealth = 10.0f;

        return actor;
    }

    public static Actor GetMob()
    {
        Actor actor = new Actor();

        actor.faction = Faction.Enemy;
        actor.weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword);
        actor.maxHealth = 2.0f;

        return actor;
    }
}