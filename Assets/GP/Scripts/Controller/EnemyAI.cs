using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent; // L'agent de navigation pour l'ennemi
    public Transform player; // Référence au joueur
    public LayerMask whatIsPlayer; // Masque de couche pour détecter le joueur
    public float health; // Santé de l'ennemi

    // Variables pour l'attaque
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // Variables pour les états de l'ennemi
    public float attackRange;
    public bool playerInAttackRange;
    public float followRange;

    // Référence à l'Animator pour gérer les animations
    public Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Récupérer l'Animator attaché à l'ennemi
    }

    private void Update()
    {
        // Si l'ennemi est mort, arrêter toute action
        if (isDead) return;

        // Vérifier si le joueur est dans les portées d'attaque et de suivi
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        bool playerInFollowRange = Physics.CheckSphere(transform.position, followRange, whatIsPlayer);

        // Si le joueur est à portée de suivi mais hors de portée d'attaque, l'ennemi le suit
        if (playerInFollowRange && !playerInAttackRange)
        {
            FollowPlayer();
        }
        // Si le joueur est à portée d'attaque, l'ennemi attaque ou suit
        if (playerInAttackRange && playerInFollowRange)
        {
            // Vérifier la distance pour savoir s'il faut suivre ou attaquer
            if (Vector3.Distance(transform.position, player.position) > attackRange * 0.75f)
            {
                FollowPlayer(); // Continue de suivre si l'ennemi est trop loin pour une attaque optimale
            }
            else
            {
                AttackPlayer(); // Attaque seulement quand l'ennemi est suffisamment proche
            }
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

    // Fonction pour attaquer le joueur
    private void AttackPlayer()
    {
        // L'ennemi arrête de bouger lorsqu'il attaque
        agent.SetDestination(transform.position);

        // Désactiver l'animation de déplacement pendant l'attaque
        animator.SetBool("isMoving", false);

        if (!alreadyAttacked)
        {
            // Créer le projectile à une position légèrement décalée vers l'avant de l'ennemi
            Vector3 spawnPosition = transform.position + transform.forward * 1.5f;
            Rigidbody rb = Instantiate(projectile, spawnPosition, Quaternion.identity).GetComponent<Rigidbody>();

            // Viser directement le joueur
            Vector3 direction = (player.position - spawnPosition).normalized;
            rb.AddForce(direction * 32f, ForceMode.Impulse);

            // Délai avant la prochaine attaque
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Réinitialise la capacité d'attaque après un délai
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Fonction appelée quand l'ennemi reçoit des dégâts
    public void TakeDamage(int damage, string hitTag)
    {
        // Si l'ennemi est déjà mort, ne rien faire
        if (isDead) return;

        health -= damage;

        // Si la santé de l'ennemi tombe à zéro ou moins, il est détruit
        if (health <= 0)
        {
            // Déterminer quel type d'impact (tête ou torse)
            if (hitTag == "Head")
            {
                // Jouer l'animation de mort par tir dans la tête
                animator.SetTrigger("dieHead");
            }
            else if (hitTag == "Torso")
            {
                // Jouer l'animation de mort par tir dans le torse
                animator.SetTrigger("dieTorso");
            }

            Die(); // Appeler la fonction de mort après l'animation
        }
    }

    // Fonction pour gérer la mort de l'ennemi
    private void Die()
    {
        // Stop le smouvements de l'ennemi
        agent.SetDestination(transform.position);
        agent.isStopped = true;

        isDead = true;

        // Détruire l'ennemi après un délai (pour permettre à l'animation de se jouer)
        Destroy(gameObject, 3f); // L'ennemi disparaît après 3 secondes
    }

    // Fonction pour infliger des dégâts au joueur
    public void InflictDamageToPlayer(int damage)
    {
        GameObject playerObj = GameObject.Find("PlayerCamera");
        if (playerObj != null)
        {
            PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Appliquer les dégâts au joueur
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rayon pour la portée d'attaque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Rayon pour la portée de suivi
    }
}


