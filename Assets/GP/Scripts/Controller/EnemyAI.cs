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

    public Transform TRA_Eye;

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
    // Fonction pour attaquer le joueur
    private void AttackPlayer()
    {
        // L'ennemi arrête de bouger lorsqu'il attaque
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Créer le projectile à une position légèrement décalée vers l'avant de l'ennemi pour un meilleur visuel
            Rigidbody rb = Instantiate(projectile, TRA_Eye.position, Quaternion.identity).GetComponent<Rigidbody>();

            // Viser directement le joueur (si vous voulez viser le corps)
            Vector3 targetPosition = player.position;

            // Calculer la direction vers le joueur
            Vector3 direction = (targetPosition - TRA_Eye.position).normalized;

            // Appliquer la force au projectile pour qu'il soit tiré directement vers le joueur
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
    public void TakeDamage(int damage, bool HeadShot = false)
    {
        health -= damage;

        // Si la santé de l'ennemi tombe à zéro ou moins, il est détruit
        if (health <= 0)
        {
            Die(HeadShot);
        }
    }

    // Fonction pour gérer la mort de l'ennemi
    public void Die(bool HeadShot = false)
    {
        if (HeadShot)
        {
            TimerManager.Instance.AddToTimer(1);
        }
        TimerManager.Instance.AddToTimer(3.5f);
        Destroy(gameObject);
    }

    public void InflictDamageToPlayer(int damage)
    {
        TimerManager.Instance.SoustractTimer(2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Rayon pour la portée d'attaque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Rayon pour la portée de suivi
    }

}


