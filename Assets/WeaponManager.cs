using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [NonSerialized] public WeaponData PlayerWeapon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public List<WeaponData> LIST_AllWeapons;
    
    WeaponData GetActualWeapon()
    {
        return PlayerWeapon;
    }
    public void NewWeapon(int Index)
    {
        PlayerWeapon = LIST_AllWeapons[Index].Clone();
    }
}
