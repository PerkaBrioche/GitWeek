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
            FollowerEnemy enemy = collision.gameObject.GetComponent<FollowerEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(hitTag);
            }

            // D�truire le projectile apr�s l'impact
            Destroy(gameObject);
        }
    }
}

