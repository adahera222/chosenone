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
    public List<Action> abilities = new List<Action>();

    // stats
    private float _maxHealth;
    public float health;
    public float damageModifier = 1.0f;
    public float defense = 1.0f;

    // inventory
    public Weapon weapon = null;

    // ================================================================================
    //  getters and setters
    // --------------------------------------------------------------------------------

    public Action currentAction
    {
        get
        {
            if (_actions.Count > 0)
            {
                return _actions[0];
            }
            else
            {
                return null;
            }
        }
    }
    public float maxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            health = maxHealth;
        }
    }        

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
        if (state != ActionState.Dead)
        {
            UpdateActionList();
        }
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

            state = ActionState.TakingAction;
            return;
        }

        currentAction.Update();

        if (currentAction.HasFinished())
        {
            currentAction.EndAction();
            _actions.RemoveAt(0);

            if (_actions.Count == 0)
            {
                state = ActionState.Idle;
            }
        }
    }

    public bool HasActions()
    {
        if (_actions.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeAction(Action action)
    {
        _actions.Add(action);
        if (action.followWithAction != null)
        {
            TakeAction(action.followWithAction);
        }
    }

    public float DealDamage(Action action)
    {
        return weapon.damage * action.delta * damageModifier;
    }

    public void ApplyDamage(float d)
    {
        health -= d;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public Action GetMainAbility()
    {
        return abilities[0];
    }

    // ================================================================================
    //  private methods
    // --------------------------------------------------------------------------------

    private void Die()
    {
        state = ActionState.Dead;
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

        actor.abilities.Add(Action.GetMeleeAction(1.0f, 0.3f));

        return actor;
    }

    public static Actor GetMob()
    {
        Actor actor = new Actor();

        actor.faction = Faction.Enemy;
        actor.weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword);
        actor.maxHealth = 3.0f;

        actor.abilities.Add(Action.GetMeleeAction(1.0f, 0.3f));

        return actor;
    }
}