using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f; // Vitesse de rotation


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x = -90;
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
