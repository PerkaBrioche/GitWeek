using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    private BulletController bulletController;
    private WeaponController WeaponController;

    private void Awake()
    {
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

        if (Input.GetKeyDown("r"))
        {
            WeaponController.Reaload();
        }
    }
}
