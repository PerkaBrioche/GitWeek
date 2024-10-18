using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f; // Vitesse de rotation

    void Update()
    {
        if (player == null) return;

        // Calculer la direction vers le joueur
        Vector3 directionToPlayer = player.position - transform.position;

        // Déterminer la rotation cible en direction du joueur
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Faire une rotation fluide vers la cible
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verrouiller l'axe X à -90° pour conserver l'orientation verticale
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x = -90;
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
