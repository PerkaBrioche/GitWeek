using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    public Transform TRA_BulletOrigin;
    public GameObject PART_Impact;
    public GameObject PART_Electric;
    public GameObject PART_Blood;
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
            if (Physics.Raycast(V3_Origin, shootDirection, out hit, Weapon.FLO_WeaponRange))
            {
                RayTouch(hit, Weapon.INT_Damage, Weapon.BOOL_CAC);
            }
            
            if(Weapon.BOOL_CAC){return;}
            var Bullet = Instantiate(PREF_Bullet, TRA_BulletOrigin.position, transform.rotation);
            Bullet.GetComponent<Bullet>().StartBullet(shootDirection);
 
        }

        ShakeManager.instance.ShakeCamera(Weapon.ShakeValue.x, Weapon.ShakeValue.y);
    }


    public void RayTouch(RaycastHit hit, int damage, bool cac)
    {
        string tag = hit.transform.tag;
        Vector3 hitPosition = hit.point;
        if (tag == "Obstacle")
        {
            Instantiate(PART_Impact, hitPosition, hit.transform.rotation);
        }

        if (tag == "Generator")
        {
            if (cac){ damage *= 2;}
            hit.transform.GetComponent<GeneratorController>().LoseLife(damage);
            Instantiate(PART_Electric, hitPosition, hit.transform.rotation);
        }

        if (tag == "Torse" || tag == "Head")
        {
            Instantiate(PART_Blood, hitPosition, hit.transform.rotation);
            if (tag == "Head")
            {
                damage *= 2;
            }
            hit.transform.GetComponent<EnemyAi>().TakeDamage(damage);
        }
    }
}