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

    public void SetToPlayer()
    {
        faction = Faction.Player;
        weapon = Weapon.GetWeapon(Weapon.WeaponType.Sword);
        maxHealth = 10.0f;
    }
}