using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet touché est un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Récupérer le tag de la partie du corps touchée
            string hitTag = collision.collider.tag;

            // Accéder au script de l'ennemi et lui infliger des dégâts
            FollowerEnemy enemy = collision.gameObject.GetComponent<FollowerEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(hitTag);
            }

            // Détruire le projectile après l'impact
            Destroy(gameObject);
        }
    }
}

