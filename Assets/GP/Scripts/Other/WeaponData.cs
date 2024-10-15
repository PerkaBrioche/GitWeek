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
    public float FLO_TimeBeetweenShoot;
    public Vector2 ShakeValue;
    
    public WeaponData Clone()
    {
        WeaponData clone = ScriptableObject.CreateInstance<WeaponData>();
        clone.STR_WeaponName = this.STR_WeaponName;
        clone.INT_BulletclipMax = this.INT_BulletclipMax;
        clone.FLO_ReloadingTime = this.FLO_ReloadingTime;
        clone.INT_Damage = this.INT_Damage;
        clone.FLO_BulletDispersion = this.FLO_BulletDispersion;
        clone.INT_BulletToShot = this.INT_BulletToShot;
        clone.FLO_TimeBeetweenShoot = this.FLO_TimeBeetweenShoot;
        clone.ShakeValue = this.ShakeValue;
        return clone;
    }
}

