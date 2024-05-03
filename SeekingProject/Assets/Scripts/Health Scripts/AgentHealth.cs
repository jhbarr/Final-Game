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
        //StartCoroutine(Hurt());
        GetComponentInChildren<Animator>().SetTrigger("hurtTrigger");
    }

    //private IEnumerator Hurt()
    //{

    //    Animator animator = GetComponentInChildren<Animator>();
    //    animator.SetBool("isAttacking", false);
    //    animator.SetBool("isFollowing", false);
    //    animator.SetBool("isWandering", false);

    //    animator.SetTrigger("hurtTrigger");

    //    bool v = animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    //    yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
    //}
}


