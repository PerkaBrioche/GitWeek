using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerActionController : MonoBehaviour
{
    private BulletController bulletController;
    private WeaponController WeaponController;
    private bool CanPlayTick;
    private FirstPersonController FirstPersonController;
    public float FLO_DashDuration;
    public float FLO_DashCoolDown;
    public float FLO_DashDistance;

    private bool CanDash;

    public TrailsController TrailsController;

    public AudioClip CLIP_EmptyMag;
    public AudioClip CLIP_Dash;

    private void Awake()
    {
        FirstPersonController = transform.GetComponent<FirstPersonController>();
        CanDash = true;
        bulletController = transform.GetComponent<BulletController>();
        WeaponController = transform.GetComponent<WeaponController>();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0) && WeaponController.HasWeapon())
        {
            if (WeaponController.HasBullet() && !WeaponController.IsReloading() && !WeaponController.TimingBeetween())
            {
                AnimationManager.Instance.PlayAnim(WeaponController.ActualWeapon.List_AnimName[1]);
                SoundManager.Instance.PlaySound(WeaponController.ActualWeapon.LIST_ShootClip[Random.Range(0, WeaponController.ActualWeapon.LIST_ShootClip.Count)]);
                bulletController.ShootBullet(WeaponController.ActualWeapon);
                CanPlayTick = true;
                WeaponController.LaunchShootTime();
                if(WeaponController.ActualWeapon.BOOL_CAC){return;}
                WeaponController.UpdateClip();
            }
            else
            {
                if (!WeaponController.HasBullet() && CanPlayTick)
                {
                    print("PLAY NO BULLET");
                    CanPlayTick = false;
                    StartCoroutine(WaitTick());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            Dash();
        }

        if (Input.GetKeyDown("r"))
        {
            WeaponController.Reaload();
        }

        if (WeaponController.HasWeapon() && WeaponController.LIST_PlayerWeapons.Count > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && WeaponController.CanScrollUp()) // forward
            {
                WeaponController.WheelWeapon(1);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && WeaponController.CanScrollDown()) // backwards
            {
                WeaponController.WheelWeapon(-1);
            }    
        }
    }

    private void Dash()
    {
        CanDash = false;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dashDirection = transform.right * horizontal + transform.forward * vertical;

        if (dashDirection != Vector3.zero)
        {
            StartCoroutine(PlayerDash(dashDirection));
        }
    }

    private IEnumerator PlayerDash(Vector3 dashDirection)
    {
        SoundManager.Instance.PlaySound(CLIP_Dash);
        Rigidbody myRb = GetComponent<Rigidbody>();
        float currentAlpha = 0f;
        float dashDuration = 0.2f; // Durée du dash
        Vector3 startPosition = transform.position;
    
        Vector3 targetPosition = startPosition + dashDirection.normalized * FLO_DashDistance;
        myRb.isKinematic = true;

        TrailsController.ChangeEmission();
        FirstPersonController.enableZoom = true;
        
        while (currentAlpha < 1)
        {
            currentAlpha += Time.deltaTime / dashDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, currentAlpha);
            yield return null;
        }
        FirstPersonController.enableZoom = false;

        TrailsController.ChangeEmission();
        myRb.isKinematic = false;
        StartCoroutine(DashCoolDown());
    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(FLO_DashCoolDown);
        CanDash = true;
    }


    private IEnumerator WaitTick()
    {
        SoundManager.Instance.PlaySound(CLIP_EmptyMag);
        yield return new WaitForSeconds(0.4f);
        CanPlayTick = true;
    }


}
