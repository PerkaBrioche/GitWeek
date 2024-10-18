using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Data", fileName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public bool BOOL_CAC;
    public string STR_WeaponName;
    public int INT_BulletclipMax;
    public int INT_ActualClip;
    public float FLO_ReloadingTime;
    public int INT_Damage;
    public float FLO_BulletDispersion;
    public int INT_BulletToShot;
    public float FLO_TimeBeetweenShoot;
    public float FLO_WeaponRange;
    public Vector2 ShakeValue;

    [Space(10)]
    [Header("Details")]
    public Texture TEXTURE_IconWeapon;
    public Texture TEXTURE_RightArm;
    public bool OneHanded;
    public List<String> List_AnimName;

    public Texture Texture_Icon;

    [Space(10)] [Header("SOUND")] 
    public List<AudioClip> LIST_ShootClip;
    public List<AudioClip> LIST_TickClip;
    public List<AudioClip> LIST_ReloadClip;

    public WeaponData Clone()
    {
        WeaponData clone = ScriptableObject.CreateInstance<WeaponData>();
        clone.STR_WeaponName = this.STR_WeaponName;
        clone.TEXTURE_IconWeapon = this.TEXTURE_IconWeapon;
        clone.Texture_Icon = this.Texture_Icon;
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
        clone.TEXTURE_RightArm = this.TEXTURE_RightArm;
        clone.List_AnimName = this.List_AnimName;
        return clone;
    }
}

