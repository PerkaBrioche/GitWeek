using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public WeaponData Weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WeaponManager.Instance.NewWeapon(Weapon);
            Destroy(gameObject);
        }
    }
}
