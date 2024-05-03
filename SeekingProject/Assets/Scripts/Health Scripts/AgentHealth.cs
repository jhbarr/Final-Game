using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    // Code related to the enemies health
    private int maxHealth = 50;
    private int currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void takeDamage(int healthLoss)
    {
        currentHealth -= healthLoss;
        healthBar.setHealth(currentHealth);
        StartCoroutine(Hurt());
    }

    private IEnumerator Hurt()
    {
        GetComponentInChildren<Animator>().SetTrigger("hurtTrigger");
        yield break;
    }
}


