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

    // ================================================================================
    //  public
    // --------------------------------------------------------------------------------

    public Faction faction;

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
    //  constructor
    // --------------------------------------------------------------------------------

    public Actor()
    {
        faction = Faction.Enemy;

        if (weapon == null)
        {
            weapon = new Weapon();
        }
    }

    // ================================================================================
    //  public methods
    // --------------------------------------------------------------------------------

    public static Actor GetPlayer()
    {
        Actor actor = new Actor();

        actor.faction = Faction.Player;
        actor.weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword);
        actor.maxHealth = 10.0f;

        return actor;
    }
}