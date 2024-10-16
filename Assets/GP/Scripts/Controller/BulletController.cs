using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform TRA_BulletOrigin;
    public GameObject PART_Impact;
    public GameObject PREF_Bullet;

    private void Start()
    {
    }

    public void ShootBullet(WeaponData Weapon)
    {
        bool shaked = false;
        Camera mainCamera = Camera.main;
        if (mainCamera == null) { return; }

        Vector3 V3_Origin = mainCamera.transform.position;

        for (int i = 0; i < Weapon.INT_BulletToShot; i++)
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            Vector3 shootDirection = ray.direction;
            shootDirection.x += Random.Range(-Weapon.FLO_BulletDispersion, Weapon.FLO_BulletDispersion);
            shootDirection.y += Random.Range(-Weapon.FLO_BulletDispersion, Weapon.FLO_BulletDispersion);

            RaycastHit hit;
            Debug.DrawRay(V3_Origin, shootDirection * Weapon.FLO_WeaponRange, Color.red, 1.0f);
            var Bullet = Instantiate(PREF_Bullet, TRA_BulletOrigin.position, transform.rotation);
            Bullet.GetComponent<Bullet>().StartBullet(shootDirection);

            if (Physics.Raycast(V3_Origin, shootDirection, out hit, Weapon.FLO_WeaponRange))
            {
                RayTouch(hit);
            }
 
        }

        ShakeManager.instance.ShakeCamera(Weapon.ShakeValue.x, Weapon.ShakeValue.y);
    }


    public void RayTouch(RaycastHit hit)
    {
        string tag = hit.transform.tag;
        Vector3 hitPosition = hit.point;
        if (tag == "Obstacle")
        {
            Instantiate(PART_Impact, hitPosition, hit.transform.rotation);
        }
    }
}