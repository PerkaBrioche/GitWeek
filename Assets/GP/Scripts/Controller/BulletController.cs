using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Transform TRA_OriginPosition;
    public float FLOAT_MaxRange = 100f; 

    private void Start()
    {
    }

    public void ShootBullet(int BulletToShot = 1, float BulletDisperion = 0)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) { return; }

        Vector3 V3_Origin = mainCamera.transform.position;

        for (int i = 0; i < BulletToShot; i++)
        {
            print("Bullet Shot " + (i + 1));

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            Vector3 shootDirection = ray.direction;
            shootDirection.x += Random.Range(-BulletDisperion, BulletDisperion);
            shootDirection.y += Random.Range(-BulletDisperion, BulletDisperion);

            RaycastHit hit;
            Debug.DrawRay(V3_Origin, shootDirection * FLOAT_MaxRange, Color.red, 1.0f);

            if (Physics.Raycast(V3_Origin, shootDirection, out hit, FLOAT_MaxRange))
            {
                Debug.Log("Objet touché : " + hit.collider.name);
            }
            ShakeManager.instance.ShakeCamera(0.25f, 0.1f);
        }
    }
}