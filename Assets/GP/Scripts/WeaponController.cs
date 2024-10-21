using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{ 
    [NonSerialized] public WeaponData ActualWeapon;
    public List<WeaponData> LIST_PlayerWeapons;
    private bool BOOL_IsReloading;
    private bool BOOL_TimingBeetween;

    public int INT_WheelWeapon;

    public TextMeshProUGUI TMP_Clip;
    public TextMeshProUGUI TMP_WeaponName;
    public IConManager IConManager;

    public ArmController ArmController;

    public AudioClip CLIP_ChangeWeapon;

    private void Start()
    {
        ArmController = FindObjectOfType<ArmController>();
    }

    public void Reaload()
    {
        if(BOOL_IsReloading || ActualWeapon.BOOL_CAC){return;}
        
        BOOL_IsReloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        AnimationManager.Instance.PlayAnim(ActualWeapon.List_AnimName[2]);
        SoundManager.Instance.PlaySound(ActualWeapon.LIST_ReloadClip[Random.Range(0, ActualWeapon.LIST_ReloadClip.Count)]);
        yield return new WaitForSeconds(ActualWeapon.FLO_ReloadingTime);
        BOOL_IsReloading = false;
        ActualWeapon.INT_ActualClip = ActualWeapon.INT_BulletclipMax;
        UpdateClip(0);
    }

    public void UpdateWeapon(WeaponData NewWeapon)
    {
        LIST_PlayerWeapons.Add(NewWeapon);
        INT_WheelWeapon = LIST_PlayerWeapons.Count-1;
        ChangeWeapon(NewWeapon);
        ActualWeapon.INT_ActualClip = ActualWeapon.INT_BulletclipMax;
    }

    public void WheelWeapon(int wheel)
    {
        INT_WheelWeapon += wheel;
        if(LIST_PlayerWeapons[INT_WheelWeapon] == null){return;}
        ChangeWeapon(LIST_PlayerWeapons[INT_WheelWeapon]);
    }

    public void ChangeWeapon(WeaponData NewWeapon)
    {
        ActualWeapon = NewWeapon;
        BOOL_TimingBeetween = false;
        BOOL_IsReloading = false;
        UpdateClip(0);
        TMP_WeaponName.text = ActualWeapon.STR_WeaponName;
        StopAllCoroutines();
        BOOL_IsReloading = false;
        IConManager.CheckIncon(INT_WheelWeapon);
        SoundManager.Instance.PlaySound(CLIP_ChangeWeapon);
        AnimationManager.Instance.PlayAnim(ActualWeapon.List_AnimName[0]);
        ArmController.ChangeArmsSkin(ActualWeapon.OneHanded);

    }

    public void UpdateClip(int ClipToSuppr = 1)
    {
        ActualWeapon.INT_ActualClip -= ClipToSuppr;
        TMP_Clip.text = "Clip : " + ActualWeapon.INT_ActualClip;
    }
    
    public void LaunchShootTime()
    {
        BOOL_TimingBeetween = true;
        StartCoroutine(TimeBeetweenShoot());
    }
    
    private IEnumerator TimeBeetweenShoot()
    {
        if (!ActualWeapon.BOOL_CAC)
        {
            SoundManager.Instance.PlaySound(ActualWeapon.LIST_TickClip[Random.Range(0, ActualWeapon.LIST_TickClip.Count)]);
        }
        yield return new WaitForSeconds(ActualWeapon.FLO_TimeBeetweenShoot);
        BOOL_TimingBeetween = false;

    }

    public bool HasBullet()
    {
        if (ActualWeapon.INT_ActualClip > 0)
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
    
    public bool HasWeapon()
    {
        if (ActualWeapon != null)
        {
            return true;
        }

        return false;
    }

    public bool CanScrollUp()
    {
        if (LIST_PlayerWeapons.Count > INT_WheelWeapon)
        {
            return true;
        }
        return false;
    }
    public bool CanScrollDown()
    {
        if (LIST_PlayerWeapons.Count > 1 && INT_WheelWeapon != 0)
        {
            return true;
        }
        return false;
    }
    
    
}
