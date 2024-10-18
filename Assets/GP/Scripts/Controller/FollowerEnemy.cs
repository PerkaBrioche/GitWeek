using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerEnemy : MonoBehaviour
{
    public NavMeshAgent agent; // L'agent de navigation pour l'ennemi
    public Transform player; // R�f�rence au joueur
    public LayerMask whatIsPlayer; // Masque de couche pour d�tecter le joueur
    public float followRange; // Port�e de suivi
    public float health = 100f; // Sant� initiale de l'ennemi

    private bool playerInRange; // V�rifie si le joueur est dans la port�e

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; 
        agent = GetComponent<NavMeshAgent>(); // R�cup�rer l'agent de navigation
    }

    private void Update()
    {
        // V�rifier si le joueur est dans la port�e de suivi
        playerInRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);

        // Si le joueur est dans la port�e, suivre
        if (playerInRange)
        {
            FollowPlayer();
        }
    }

    // Fonction pour suivre le joueur
    private void FollowPlayer()
    {
        // L'ennemi se d�place vers la position du joueur
        agent.SetDestination(player.position);
    }

    // Fonction pour que l'ennemi prenne des d�g�ts
    public void TakeDamage(float damage)
    {
        health -= damage; // R�duire la sant�

        // Si la sant� tombe � 0 ou moins, l'ennemi meurt
        if (health <= 0)
        {
            Die(); // Appeler la fonction de mort
        }
    }

    // Fonction pour g�rer la mort de l'ennemi
    private void Die()
    {
        // Tu peux ajouter une animation de mort ici avant de d�truire l'ennemi
        Destroy(gameObject); // D�truire l'ennemi
    }

    // Fonction pour dessiner des gizmos dans l'�diteur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la port�e de suivi
    }
}


