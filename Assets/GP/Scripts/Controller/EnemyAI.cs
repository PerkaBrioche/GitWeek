using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // navigation de l'IA

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float detectionRange = 10f; 

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // si le joueur est à portée, l'ennemi se déplace vers lui
        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            // sinon, l'ennemi s'arrête
            agent.ResetPath();
        }
    }
}

