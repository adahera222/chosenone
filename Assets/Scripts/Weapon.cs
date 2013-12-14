using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon {

    public enum WeaponType {
        Sword,
        Axe,
    }

    public string name = "Stick";
    public float damage = 0.1f;

    public static Weapon GetWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Sword:
                return new Weapon() { name="Sword", damage = 2.0f };
            case WeaponType.Axe:
                return new Weapon() { name = "Axe", damage = 3.0f };
            default:
                return new Weapon() { };
        }
    }
}