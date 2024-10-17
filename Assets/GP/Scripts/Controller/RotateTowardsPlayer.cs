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

            // Appliquer la rotation fluide
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Conserver l'axe Y du plane en vertical
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            eulerRotation.x = -90; // Fixer l'axe X à -90° pour garder le plane vertical
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }
}






