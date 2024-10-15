using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform TRA_OriginPosition;

    private void Start()
    {
    }

    public void ShootBullet(WeaponData Weapon)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) { return; }

        Vector3 V3_Origin = mainCamera.transform.position;

        for (int i = 0; i < Weapon.INT_BulletToShot; i++)
        {
            print("Bullet Shot " + (i + 1));

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            Vector3 shootDirection = ray.direction;
            shootDirection.x += Random.Range(-Weapon.FLO_BulletDispersion, Weapon.FLO_BulletDispersion);
            shootDirection.y += Random.Range(-Weapon.FLO_BulletDispersion, Weapon.FLO_BulletDispersion);

            RaycastHit hit;
            Debug.DrawRay(V3_Origin, shootDirection * Weapon.FLO_WeaponRange, Color.red, 1.0f);

            if (Physics.Raycast(V3_Origin, shootDirection, out hit, Weapon.FLO_WeaponRange))
            {
                Debug.Log("Objet touché : " + hit.collider.name);
            }
            ShakeManager.instance.ShakeCamera(Weapon.ShakeValue.x, Weapon.ShakeValue.y);
        }
    }
}