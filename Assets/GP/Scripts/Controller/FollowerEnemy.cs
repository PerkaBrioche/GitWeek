using System.Collections;
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

    public Animator animator; // R�f�rence � l'Animator pour g�rer les animations
    public bool isDead = false; // V�rifier si l'ennemi est mort

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform; // Trouver la cam�ra du joueur
        agent = GetComponent<NavMeshAgent>(); // R�cup�rer l'agent de navigation
        animator = GetComponent<Animator>(); // R�cup�rer l'Animator attach� � l'ennemi
    }

    private void Update()
    {
        // Si l'ennemi est mort, arr�ter toute action
        if (isDead) return;

        // V�rifier si le joueur est dans la port�e de suivi
        playerInRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);

        // Si le joueur est dans la port�e, suivre
        if (playerInRange)
        {
            FollowPlayer();
        }
        else
        {
            // Si l'ennemi ne suit pas, passer � l'animation idle (attente)
            animator.SetBool("isMoving", false);
        }
    }

    // Fonction pour suivre le joueur
    private void FollowPlayer()
    {
        // L'ennemi se d�place vers la position du joueur
        agent.SetDestination(player.position);

        // Activer l'animation de d�placement
        animator.SetBool("isMoving", true);
    }

    // Fonction appel�e lorsque l'ennemi est touch�
    public void TakeDamage(string hitTag)
    {
        // Si l'ennemi est d�j� mort, ne rien faire
        if (isDead) return;

        // D�terminer quel type d'impact (t�te ou torse)
        if (hitTag == "Head")
        {
            // Jouer l'animation de mort par tir dans la t�te
            animator.SetTrigger("dieHead");
            Die(); // Appeler la fonction de mort apr�s l'animation
        }
        else if (hitTag == "Torso")
        {
            // Jouer l'animation de mort par tir dans le torse
            animator.SetTrigger("dieTorso");
            Die(); // Appeler la fonction de mort apr�s l'animation
        }
    }

    // Fonction pour g�rer la mort de l'ennemi
    private void Die()
    {
        // Arr�ter tout mouvement
        agent.SetDestination(transform.position);
        agent.isStopped = true;

        // D�sactiver toute autre action
        isDead = true;

        // D�truire l'ennemi apr�s un d�lai (pour permettre l'animation de mort de se jouer)
        Destroy(gameObject, 3f); // L'ennemi dispara�t apr�s 3 secondes
    }

    // Fonction pour dessiner des gizmos dans l'�diteur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la port�e de suivi
    }
}
