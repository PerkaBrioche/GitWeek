using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public bool BOOL_CAC;
    public bool OneHanded;
    public string STR_WeaponName;
    public int INT_BulletclipMax;
    public int INT_ActualClip;
    public float FLO_ReloadingTime;
    public int INT_Damage;
    public float FLO_BulletDispersion;
    public int INT_BulletToShot;
    public float FLO_TimeBeetweenShoot;
    public float FLO_WeaponRange;
    public Texture TEXTURE_IconWeapon;
    public Vector2 ShakeValue;
    
    public WeaponData Clone()
    {
        WeaponData clone = ScriptableObject.CreateInstance<WeaponData>();
        clone.STR_WeaponName = this.STR_WeaponName;
        clone.TEXTURE_IconWeapon = this.TEXTURE_IconWeapon;
        clone.OneHanded = this.OneHanded;
        clone.BOOL_CAC = this.BOOL_CAC;
        clone.INT_BulletclipMax = this.INT_BulletclipMax;
        clone.INT_ActualClip = this.INT_ActualClip;
        clone.FLO_ReloadingTime = this.FLO_ReloadingTime;
        clone.INT_Damage = this.INT_Damage;
        clone.FLO_BulletDispersion = this.FLO_BulletDispersion;
        clone.INT_BulletToShot = this.INT_BulletToShot;
        clone.FLO_TimeBeetweenShoot = this.FLO_TimeBeetweenShoot;
        clone.ShakeValue = this.ShakeValue;
        clone.FLO_WeaponRange = this.FLO_WeaponRange;
        return clone;
    }
}

