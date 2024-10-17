using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowerEnemy : MonoBehaviour
{
    public NavMeshAgent agent; // L'agent de navigation pour l'ennemi
    public Transform player; // Référence au joueur
    public LayerMask whatIsPlayer; // Masque de couche pour détecter le joueur

    public float followRange; // Portée de suivi
    public float attackRange; // Portée d'attaque
    private bool playerInRange; // Vérifie si le joueur est dans la portée

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; // Trouver la caméra du joueur
        agent = GetComponent<NavMeshAgent>(); // Récupérer l'agent de navigation
    }

    private void Update()
    {
        // Vérifier si le joueur est dans la portée de suivi
        playerInRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);

        // Si le joueur est dans la portée, suivre
        if (playerInRange)
        {
            FollowPlayer();
        }
    }

    // Fonction pour suivre le joueur
    private void FollowPlayer()
    {
        // L'ennemi se déplace vers la position du joueur
        agent.SetDestination(player.position);
    }

    // Fonction pour dessiner des gizmos dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la portée de suivi
    }
}

