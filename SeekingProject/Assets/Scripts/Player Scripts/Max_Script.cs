using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Max_Script : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer sr;

    public ParticleSystem ps;
    public GameObject particleSystemObject;
    ParticleSystem.EmissionModule emission;

    public Vector2 movement;
    private bool facingLeft;

    public float activeMoveSpeed;

    public bool jumping;
    public float jumpSpeed;

    public float jumpLength = 0.5f;
    public float jumpCooldown = 1f;

    public float jumpCounter { get; private set; }
    public float jumpCdCount { get; private set; }

    public float biteLength;
    public float biteCooldown;

    public float biteCounter { get; private set; }
    public float biteCdCount { get; private set; }

    // Variables necessary for the attack funcitonality
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayerMask;

    public AudioSource chomp;

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        //-----------Basic Movement-------------------//

        if (!jumping)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        movement.Normalize();

        rb.velocity = movement * activeMoveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

        if (movement.x < 0)
        {
            facingLeft = true;
        }
        else if (movement.x > 0)
        {
            facingLeft = false;
        }
        sr.flipX = facingLeft;


        //-----------Jumping--------------------------//

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCdCount <= 0 && jumpCounter <= 0)
            {
                animator.SetBool("IsJumping", true);
                jumping = true;
                activeMoveSpeed = jumpSpeed;
                jumpCounter = jumpLength;
            }
        }

        if (jumpCounter > 0)
        {
            // In the jump
            jumpCounter -= Time.deltaTime;

            if (rb.velocity.Equals(Vector2.zero))
            {
                if (facingLeft)
                {
                    rb.velocity = Vector2.left * activeMoveSpeed;
                }
                else
                {
                    rb.velocity = Vector2.right * activeMoveSpeed;
                }

            }

            if (jumpCounter <= 0)
            {
                // Jump finished
                activeMoveSpeed = moveSpeed;
                jumping = false;
                animator.SetBool("IsJumping", false);

                jumpCdCount = jumpCooldown;
            }
        }

        if (jumpCdCount > 0)
        {
            jumpCdCount -= Time.deltaTime;
        }


        //-----------Meele Attack---------------------//

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (biteCdCount <= 0 && biteCounter <= 0)
            {
                animator.SetBool("IsBiting", true);
                biteCounter = biteLength;
            }
        }

        // ----- Splash attack --------//
        if (Input.GetKeyDown(KeyCode.M))
        {
            splashAttack();
        }


        if (biteCounter > 0)
        {
            biteCounter -= Time.deltaTime;

            if (biteCounter <= 0)
            {
                // -------Attack functionality--------
                attack();
            }
        }

        if (biteCdCount > 0)
        {
            biteCdCount -= Time.deltaTime;
        }

    }


    private void splashAttack()
    {
        particleSystemObject.transform.position = gameObject.transform.position;

        var em = ps.emission;
        var dur = ps.duration;

        ps.Play();
        emission = ps.emission;
        emission.enabled = true;
        Invoke(nameof(stopParticles), dur);
    }

    private void stopParticles()
    {
        ps.Stop();
    }


    private void attack()
    {
        // Play attack animation
        animator.SetBool("IsBiting", false);
        biteCdCount = biteCooldown;

        // Detect enemies within range of the attack
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayerMask);

        // Damage those enemies
        foreach (Collider2D collider in enemiesHit)
        {
            collider.GetComponent<AgentHealth>().takeDamage(25);
        }
    }

    public void playChompSound()
    {
        chomp.Play();
    }
}
