using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [NonSerialized] public WeaponData PlayerWeapon;
    public WeaponController WeaponController;

    public Transform TRA_Horizontal;
    public GameObject GO_Icon;

    private void Awake()
    {
        if (Instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        WeaponController = FindObjectOfType<WeaponController>();
    }
    public List<WeaponData> LIST_AllWeapons;
    
    WeaponData GetActualWeapon()
    {
        return PlayerWeapon;
    }
    public void NewWeapon(WeaponData weapon)
    {
        var Incon = Instantiate(GO_Icon, TRA_Horizontal);
        Incon.GetComponent<IconController>().SetIcon(weapon.TEXTURE_IconWeapon);
        WeaponController.UpdateWeapon(weapon);
    }
}
