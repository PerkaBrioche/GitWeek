using System.Collections;
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

    public Animator animator; // Référence à l'Animator pour gérer les animations
    public bool isDead = false; // Vérifier si l'ennemi est mort

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; // Trouver la caméra du joueur
        agent = GetComponent<NavMeshAgent>(); // Récupérer l'agent de navigation
        animator = GetComponent<Animator>(); // Récupérer l'Animator attaché à l'ennemi
    }

    private void Update()
    {
        // Si l'ennemi est mort, arrêter toute action
        if (isDead) return;

        // Vérifier si le joueur est dans la portée de suivi
        playerInRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);

        // Si le joueur est dans la portée, suivre
        if (playerInRange)
        {
            FollowPlayer();
        }
        else
        {
            // Si l'ennemi ne suit pas, passer à l'animation idle (attente)
            animator.SetBool("isMoving", false);
        }
    }

    // Fonction pour suivre le joueur
    private void FollowPlayer()
    {
        // L'ennemi se déplace vers la position du joueur
        agent.SetDestination(player.position);

        // Activer l'animation de déplacement
        animator.SetBool("isMoving", true);
    }

    // Fonction appelée lorsque l'ennemi est touché
    public void TakeDamage(string hitTag)
    {
        // Si l'ennemi est déjà mort, ne rien faire
        if (isDead) return;

        // Déterminer quel type d'impact (tête ou torse)
        if (hitTag == "Head")
        {
            // Jouer l'animation de mort par tir dans la tête
            animator.SetTrigger("dieHead");
            Die(); // Appeler la fonction de mort après l'animation
        }
        else if (hitTag == "Torso")
        {
            // Jouer l'animation de mort par tir dans le torse
            animator.SetTrigger("dieTorso");
            Die(); // Appeler la fonction de mort après l'animation
        }
    }

    // Fonction pour gérer la mort de l'ennemi
    private void Die()
    {
        // Arrêter tout mouvement
        agent.SetDestination(transform.position);
        agent.isStopped = true;

        // Désactiver toute autre action
        isDead = true;

        // Détruire l'ennemi après un délai (pour permettre l'animation de mort de se jouer)
        Destroy(gameObject, 3f); // L'ennemi disparaît après 3 secondes
    }

    // Fonction pour dessiner des gizmos dans l'éditeur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la portée de suivi
    }
}
