using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Santé maximale du joueur
    private int currentHealth; // Santé actuelle du joueur
    private void Start()
    {
        currentHealth = maxHealth; // Initialise la santé actuelle à la santé maximale
    }
    // Fonction pour appliquer des dégâts au joueur
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Réduire la santé actuelle par le montant des dégâts
        // Vérifier si la santé tombe à zéro ou moins
        if (currentHealth <= 0)
        {
            Die();
        }
        // Affiche la santé actuelle dans la console
        Debug.Log("Current Health: " + currentHealth);
    }
    // Fonction appelée lorsque le joueur meurt
    private void Die()
    {
        Debug.Log("Player is dead!");
        gameObject.SetActive(false); // Désactive l'objet joueur
    }
}
