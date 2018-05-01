using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    [SerializeField] int damage = 10;


    public int GetDamage()
    {
        return damage;
    }
}
