using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent; 

    public Transform player; 

    public LayerMask whatIsPlayer; 

    public float health; // Santé de l'ennemi, pour plus tard

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

        // Si le joueur est à portée d'attaque mais que l'ennemi n'est pas assez proche, l'ennemi continue à le suivre
        if (playerInAttackRange && playerInFollowRange)
        {
            // Si l'ennemi est assez proche (dans la portée d'attaque), il attaque
            if (Vector3.Distance(transform.position, player.position) > attackRange * 0.75f)
            {
                FollowPlayer(); // Continue de suivre si l'ennemi est encore trop loin pour une attaque optimale
            }
            else
            {
                AttackPlayer(); // Attaque seulement quand l'ennemi est suffisamment proche
            }
        }
    }


    // Fonction pour suivre le joueur quand il est à portée de suivi
    private void FollowPlayer()
    {
        // L'ennemi se déplace vers la position du joueur
        agent.SetDestination(player.position);
    }

    // Fonction pour attaquer le joueur quand il est à portée d'attaque
    private void AttackPlayer()
    {
        // L'ennemi arrête de bouger lorsqu'il attaque
        agent.SetDestination(transform.position);

        // L'ennemi se tourne vers le joueur
        transform.LookAt(player);

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

    // Réinitialise la capacité d'attaque après un délai, pour plus tard
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Fonction appelée quand l'ennemi reçoit des dégâts, pour plus tard
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Si la santé de l'ennemi tombe à zéro ou moins, il est détruit
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    // Détruit l'ennemi, pour plus tard
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rayon pour la portée d'attaque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Rayon pour la portée de suivi
    }
}


