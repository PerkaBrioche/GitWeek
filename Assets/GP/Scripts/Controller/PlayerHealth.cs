using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Sant� maximale du joueur
    private int currentHealth; // Sant� actuelle du joueur
    private void Start()
    {
        currentHealth = maxHealth; // Initialise la sant� actuelle � la sant� maximale
    }
    // Fonction pour appliquer des d�g�ts au joueur
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // R�duire la sant� actuelle par le montant des d�g�ts
        // V�rifier si la sant� tombe � z�ro ou moins
        if (currentHealth <= 0)
        {
            Die();
        }
        // Affiche la sant� actuelle dans la console
        Debug.Log("Current Health: " + currentHealth);
    }
    // Fonction appel�e lorsque le joueur meurt
    private void Die()
    {
        Debug.Log("Player is dead!");
        gameObject.SetActive(false); // D�sactive l'objet joueur
    }
}
