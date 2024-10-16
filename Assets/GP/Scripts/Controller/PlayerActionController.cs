using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private BulletController bulletController;
    private WeaponController WeaponController;
    private FirstPersonController FirstPersonController;
    public float FLO_DashDuration;
    public float FLO_DashCoolDown;
    public float FLO_DashDistance;

    private bool CanDash;

    public TrailsController TrailsController;

    private void Awake()
    {
        FirstPersonController = transform.GetComponent<FirstPersonController>();
        CanDash = true;
        bulletController = transform.GetComponent<BulletController>();
        WeaponController = transform.GetComponent<WeaponController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            WeaponManager.Instance.NewWeapon(0);
        }
        if (Input.GetKeyDown("1"))
        {
            WeaponManager.Instance.NewWeapon(1);
        }
        if (Input.GetKeyDown("2"))
        {
            WeaponManager.Instance.NewWeapon(2);
        }
        if (Input.GetMouseButton(0))
        {
            if (WeaponController.HasBullet() && !WeaponController.IsReloading() && !WeaponController.TimingBeetween()) 
            {
                bulletController.ShootBullet(WeaponController.ActualWeapon);
                WeaponController.UpdateClip();
                WeaponController.LaunchShootTime();
            }
            else
            {
                // ANIM NO BULLET LEFT || SOUND
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

}
