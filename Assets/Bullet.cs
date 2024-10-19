using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    public float Speed;

    public void StartBullet(Vector3 Direction)
    {
        direction = Direction.normalized;
    }

    private void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}