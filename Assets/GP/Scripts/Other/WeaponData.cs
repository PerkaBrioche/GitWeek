using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string STR_WeaponName;
    public int INT_BulletclipMax;
    public float FLO_ReloadingTime;
    public int INT_Damage;
    public float FLO_BulletDispersion;
    public int INT_BulletToShot;
}
