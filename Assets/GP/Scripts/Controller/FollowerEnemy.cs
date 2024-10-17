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
    public float attackRange; // Port�e d'attaque
    private bool playerInRange; // V�rifie si le joueur est dans la port�e

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; // Trouver la cam�ra du joueur
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

    // Fonction pour dessiner des gizmos dans l'�diteur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la port�e de suivi
    }
}

