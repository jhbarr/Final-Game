using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject GameOverScreen;
    public AIData aiData;

    // Code related to the enemies health
    private int maxHealth = 50;
    private int currentHealth;

    public float hurtLength;
    private float hurtCounter;

    private float healtCounter = 5;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    private void Update()
    {
        // Make it so that the stun lasts a certain number of seconds
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isHurting", false);
        }

        // Slowly regenerate the health over time
        // Five health points are restored every five seconds
        if (healtCounter < 0 && currentHealth <= maxHealth)
        {
            currentHealth += 5;
            healthBar.setHealth(currentHealth);
            healtCounter = 5;
        }
        else
        {
            healtCounter -= Time.deltaTime;
        }
    }

    // Have the enemy take damage
    // Enemy will run death logic if their heatlh dips below zero
    public void takeDamage(int healthLoss)
    {
        currentHealth -= healthLoss;
        healthBar.setHealth(currentHealth);

        if (hurtCounter <= 0)
        {
            if (GetComponentInChildren<Animator>().GetBool("isHurting") == false)
            {
                GetComponentInChildren<Animator>().SetBool("isHurting", true);
            }
            hurtCounter = hurtLength;
            healtCounter = 5;
        }

        if (currentHealth <= 0)
        {
            // Death logic
            // Come back to later
            Die();
        }
    }

    private void Die()
    {
        aiData.isDead = true;
        GetComponentInChildren<Animator>().SetTrigger("isDead");
        
        GameOverScreen.SetActive(true);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}


