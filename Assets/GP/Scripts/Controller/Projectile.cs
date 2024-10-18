using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // V�rifier si l'objet touch� est un ennemi
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // R�cup�rer le tag de la partie du corps touch�e
            string hitTag = collision.collider.tag;

            // Acc�der au script de l'ennemi et lui infliger des d�g�ts
            EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();
            if (enemy != null)
            {
                enemy.TakeDamage(10, hitTag); // Appliquer les d�g�ts avec le tag de la partie touch�e
            }

            // D�truire le projectile apr�s l'impact
            Destroy(gameObject);
        }
    }
}

