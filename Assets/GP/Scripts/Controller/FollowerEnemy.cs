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
    public Animator ANIM_Ennemy; // V�rifie si le joueur est dans la port�e

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
        print("DIEEEEEEEEEEEEEEEE");
        if (HeadShot)
        {
            ANIM_Ennemy.Play("EF_DeadHead");
            TimerManager.Instance.AddToTimer(1);
        }
        else
        {
            ANIM_Ennemy.Play("EF_DeadTorse");
        }

        this.enabled = false;

        TimerManager.Instance.AddToTimer(3.5f);
        Destroy(gameObject, 1);
    }

    // Fonction pour dessiner des gizmos dans l'�diteur
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange); // Dessiner la port�e de suivi
    }
    
    public bool IsDead(int Damage)
    {
        var alife = health;
        var cl = alife - Damage;
        print(cl);
        if (cl <= 0)
        {
            return true;
        }

        return false;
    }
}


