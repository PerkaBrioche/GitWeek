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
        // Logique de mort (animation, son, respawn, changement de scene, ect)
        Debug.Log("Player is dead!");

        gameObject.SetActive(false); // D�sactive l'objet joueur
        // Ou ajoutez votre logique de fin de jeu ici
    }

    // Fonction pour restaurer la sant� (facultatif)
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ne pas d�passer la sant� maximale
        }
    }
}

