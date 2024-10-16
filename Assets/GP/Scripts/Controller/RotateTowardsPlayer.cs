using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player; 
    public float rotationSpeed = 5f; // Vitesse de rotation

    void Update()
    {
        if (player != null)
        {
            // Calculer la direction vers le joueur
            Vector3 directionToPlayer = player.position - transform.position;

            // Créer une rotation vers le joueur
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Rotation fluide vers le joueur
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

