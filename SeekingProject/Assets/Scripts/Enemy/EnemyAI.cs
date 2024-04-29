using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{ 
    [SerializeField]
    private List<SteeringBehavior> seekSteeringBehaviors;

    [SerializeField]
    private List<SteeringBehavior> wanderSteeringBehaviors;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.001f, aiUpdateDelay = 0.001f;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextBehavior movementDirectionSolver;

    private Agent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // Invoke this method every "detectionDelay" seconds
        // The method will detect the player and obstacles around
        InvokeRepeating("PerformDetection", 0, detectionDelay);
    }

    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }

    private void Update()
    {
        if (aiData.currentTarget == null)
        {
            if (aiData.isWandering == false)
            {
                aiData.isWandering = true;
                animator.SetBool("isFollowing", true);
                StartCoroutine(Wander());
            }
        }
        if (aiData.currentTarget != null)
        {
            if (aiData.isFollowing == false)
            {
                aiData.isFollowing = true;
                animator.SetBool("isFollowing", aiData.isFollowing);
                StartCoroutine(Chase());
            }
        }
        else if (aiData.getTargetCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
        
    }

    private IEnumerator Chase()
    {
        if (aiData.currentTarget == null)
        {
            // Stop the agent
            movementInput = Vector2.zero;
            agent.MovementInput = movementInput;
            aiData.currentTarget = null;

            aiData.isFollowing = false;
            animator.SetBool("isFollowing", aiData.isFollowing);
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < 1.5f)
            {
                movementInput = Vector2.zero;
                agent.MovementInput = movementInput;

                if (aiData.isAttacking == false)
                {
                    aiData.isAttacking = true;
                    StartCoroutine(Attack());
                }

                yield break;
            }
            else
            {
                // Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(seekSteeringBehaviors, aiData);
                agent.MovementInput = movementInput;
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(Chase());
            }
        }
    }

    private IEnumerator Wander()
    {
        if (aiData.currentTarget != null)
        {
            // Make the enemy stop wandering
            aiData.isWandering = false;
            yield break;
        }
        else
        {
            movementInput = movementDirectionSolver.GetDirectionToMove(wanderSteeringBehaviors, aiData);
            agent.MovementInput = movementInput;
            yield return new WaitForSeconds(aiUpdateDelay);
            StartCoroutine(Wander());
        }
    }

    private IEnumerator Attack()
    {
        float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

        if (distance > 1.5f)
        {
            aiData.isAttacking = false;
            aiData.isFollowing = false;
            animator.SetBool("isFollowing", aiData.isFollowing);
            yield break;
        }
        else
        {
            animator.SetTrigger("attack");
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Attack());
            //animator.ResetTrigger("attack");
        }
    }
}


