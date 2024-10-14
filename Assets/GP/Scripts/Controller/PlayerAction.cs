using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private BulletController bulletController;

    private void Awake()
    {
        bulletController = FindObjectOfType<BulletController>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bulletController.ShootBullet();
        }
    }
}
