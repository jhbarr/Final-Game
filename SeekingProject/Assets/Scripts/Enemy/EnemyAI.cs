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
        // Check if the enemy shoule be wandering
        if (aiData.currentTarget == null)
        {
            // Enemy should wander if it isn't already
            if (aiData.isWandering == false)
            {
                aiData.isWandering = true;
                animator.SetBool("isWandering", aiData.isWandering);
                // Start the coroutine
                StartCoroutine(Wander());
            }
        }

        // Check if the enemy should be attacking
        // It should attack if the current target is within the attack threshold
        if (aiData.currentTarget != null && aiData.distanceToTarget <= 1.5f)
        {
            // Enemy should attack if it isn't already
            if (aiData.isAttacking == false)
            {
                aiData.isAttacking = true;
                animator.SetBool("isAttacking", aiData.isAttacking);
                // Start the coroutine
                StartCoroutine(Attack());
            }
        }

        // Check if the enemy should be chasing
        // It should still be chasing the player if the current target is not within the attack threshold
        if (aiData.currentTarget != null && aiData.distanceToTarget > 1.5f)
        {
            // Enemy should attack
            if (aiData.isFollowing == false)
            {
                aiData.isFollowing = true;
                animator.SetBool("isFollowing", aiData.isFollowing);
                // Start the coroutine
                StartCoroutine(Chase());
            }
        }
        else if (aiData.getTargetCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }



        //if (aiData.currentTarget == null)
        //{
        //    if (aiData.isWandering == false)
        //    {
        //        aiData.isWandering = true;
        //        animator.SetBool("isFollowing", true);
        //        StartCoroutine(Wander());
        //    }
        //}

        //if (aiData.currentTarget != null)
        //{
        //    if (aiData.isFollowing == false)
        //    {
        //        aiData.isFollowing = true;
        //        animator.SetBool("isFollowing", aiData.isFollowing);
        //        StartCoroutine(Chase());
        //    }
        //}
        //else if (aiData.getTargetCount() > 0)
        //{
        //    aiData.currentTarget = aiData.targets[0];
        //}

    }







    private IEnumerator Chase()
    {
        if (aiData.currentTarget == null)
        {
            // Stop the agent
            movementInput = Vector2.zero;
            agent.MovementInput = movementInput;
            aiData.currentTarget = null; // I don't think this is necessary

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

                aiData.isFollowing = false;
                animator.SetBool("isFollowing", aiData.isFollowing);

                aiData.distanceToTarget = distance;

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
            animator.SetBool("isWandering", aiData.isWandering);
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
            aiData.distanceToTarget = float.PositiveInfinity;

            aiData.isAttacking = false;
            animator.SetBool("isAttacking", aiData.isAttacking);
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Attack());
            //animator.ResetTrigger("attack");
        }
    }
}


