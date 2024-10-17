using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;
    public float health; // Santé de l'ennemi

    // Variables pour l'attaque
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // Variables pour les états de l'ennemi
    public float attackRange;
    public bool playerInAttackRange;
    public float followRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerCamera").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
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
    }

    // Fonction pour attaquer le joueur
    private void AttackPlayer()
    {
        // L'ennemi arrête de bouger lorsqu'il attaque
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Créer le projectile et le lancer vers le joueur
            Rigidbody rb = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity).GetComponent<Rigidbody>();

            // Tirer le projectile directement vers le joueur
            Vector3 direction = (player.position - transform.position).normalized;
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
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Si la santé de l'ennemi tombe à zéro ou moins, il est détruit
        if (health <= 0)
        {
            Die();
        }
    }

    // Fonction pour gérer la mort de l'ennemi
    public void Die()
    {
        // Logique de mort (animation, son, etc.)
        Destroy(gameObject); // Détruire l'ennemi
    }

    // Fonction pour infliger des dégâts au joueur
    public void InflictDamageToPlayer(int damage)
    {
        // Trouver le joueur
        GameObject playerObj = GameObject.Find("PlayerCamera");
        if (playerObj != null)
        {
            // Assurez-vous que le joueur a un script pour gérer la santé
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





