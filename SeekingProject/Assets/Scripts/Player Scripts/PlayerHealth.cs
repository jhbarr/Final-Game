using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public GameObject GameOverScreen;
    public AudioSource ouch;

    public readonly int maximumHp = 5;
    public int currentHp;


    public float hurtLength;
    private float hurtCounter;

    private bool isDead = false;


    private void Start()
    {
        currentHp = maximumHp;
    }

    public void Update()
    {
        if (hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("isHurting", false);
        }
    }

    public void takeDamage(int n)
    {
        if (hurtCounter <= 0)
        {

            currentHp -= n;
            if (currentHp <= 0)
            {
                currentHp = 0;
            }

            animator.SetBool("isHurting", true);
            hurtCounter = hurtLength;

            if (!isDead)
            {
                ouch.Play();
            }
        }

        if (currentHp <= 0)
        {
            die();
        }
    }

    public void die()
    {
        isDead = true;
        animator.SetBool("isHurting", false);
        animator.SetBool("isDead", true);

        GameOverScreen.SetActive(true);
    }
}
