using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 0.1f, acceleration = 0.05f, decelleration = 10f;

    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 currentMovementInput { get; set; }

    private void FixedUpdate()
    {
        if (currentMovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = currentMovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= decelleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = oldMovementInput * currentSpeed;
    }

}
