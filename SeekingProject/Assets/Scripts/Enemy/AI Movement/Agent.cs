using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentMover agentMover;

    private Vector2 movementInput;

    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Awake()
    {
        //agentMover = GetComponent<AgentMover>();
    }

    // Update is called once per frame
    void Update()
    {
        agentMover.currentMovementInput = movementInput;
    }
}
