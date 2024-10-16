using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{ 
    [NonSerialized] public WeaponData ActualWeapon;
    private int INT_Clip;
    private bool BOOL_IsReloading;
    private bool BOOL_TimingBeetween;

    public TextMeshProUGUI TMP_Clip;
    public TextMeshProUGUI TMP_WeaponName;

    public void Reaload()
    {
        if(BOOL_IsReloading){return;}
        
        BOOL_IsReloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(ActualWeapon.FLO_ReloadingTime);
        BOOL_IsReloading = false;
        INT_Clip = ActualWeapon.INT_BulletclipMax;
        UpdateClip(0);
    }

    public void UpdateWeapon(WeaponData NewWeapon)
    {
        ActualWeapon = NewWeapon;
        INT_Clip = ActualWeapon.INT_BulletclipMax;
        UpdateClip(0);
        TMP_WeaponName.text = ActualWeapon.STR_WeaponName;
    }

    public void UpdateClip(int ClipToSuppr = 1)
    {
        INT_Clip-= ClipToSuppr;
        TMP_Clip.text = "Clip : " + INT_Clip;
    }
    
    public void LaunchShootTime()
    {
        BOOL_TimingBeetween = true;
        StartCoroutine(TimeBeetweenShoot());
    }
    
    private IEnumerator TimeBeetweenShoot()
    {
        yield return new WaitForSeconds(ActualWeapon.FLO_TimeBeetweenShoot);
        BOOL_TimingBeetween = false;
    }

    public bool HasBullet()
    {
        if (INT_Clip > 0)
        {
            return true;
        }
        return false;
    }
    public bool TimingBeetween()
    {
        return BOOL_TimingBeetween;
    }

    public bool IsReloading()
    {
        return BOOL_IsReloading;
    }
    
    
}
