using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Max_Script : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer sr;

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

        if (biteCounter > 0)
        {
            biteCounter -= Time.deltaTime;

            if (biteCounter <= 0)
            {
                animator.SetBool("IsBiting", false);

                biteCdCount = biteCooldown;
            }
        }

        if (biteCdCount > 0)
        {
            biteCdCount -= Time.deltaTime;
        }

    }
}
