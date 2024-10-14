using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Transform TRA_OriginPosition;
    public float FLOAT_MaxRange;

    private Vector3 V3_Origin;

    private void Start()
    {
    }

    public void ShootBullet()
    {
        V3_Origin = TRA_OriginPosition.position;
        Vector3 V3_shootDirection = TRA_OriginPosition.forward;
        RaycastHit hit;
        Debug.DrawRay(V3_Origin, V3_shootDirection * FLOAT_MaxRange, Color.red, 10);

        if (Physics.Raycast(V3_Origin, V3_shootDirection * FLOAT_MaxRange, out hit))
        {
            print(hit.collider);
        }
        ShakeManager.instance.ShakeCamera(0.25f, 0.1f);
    }
}
