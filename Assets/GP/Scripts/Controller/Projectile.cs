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
            EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();
            if (enemy != null)
            {
                enemy.TakeDamage(10, hitTag); // Appliquer les dégâts avec le tag de la partie touchée
            }

            // Détruire le projectile après l'impact
            Destroy(gameObject);
        }
    }
}

