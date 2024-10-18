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
    public float health = 100f; // Santé initiale de l'ennemi

    private bool playerInRange; // Vérifie si le joueur est dans la portée

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; 
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

    // Fonction pour que l'ennemi prenne des dégâts
    public void TakeDamage(float damage)
    {
        health -= damage; // Réduire la santé

        // Si la santé tombe à 0 ou moins, l'ennemi meurt
        if (health <= 0)
        {
            Die(); // Appeler la fonction de mort
        }
    }

    // Fonction pour gérer la mort de l'ennemi
    private void Die()
    {
        // Tu peux ajouter une animation de mort ici avant de détruire l'ennemi
        Destroy(gameObject); // Détruire l'ennemi
    }

    // Fonction pour dessiner des gizmos dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la portée de suivi
    }
}


