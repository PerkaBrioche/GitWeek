using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private BulletController bulletController;

    public int Bullet;
    public float Disperion;

    private void Awake()
    {
        bulletController = FindObjectOfType<BulletController>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletController.ShootBullet(Bullet, Disperion);
        }
    }
}
