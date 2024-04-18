using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{ 
    [SerializeField]
    private List<SteeringBehavior> steeringBehaviors;

    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.01f, aiUpdateDelay = 0.01f;

    [SerializeField]
    private Vector2 movementInput;

    [SerializeField]
    private ContextBehavior movementDirectionSolver;

    private Agent agent;
    private Animator animator;

    bool isFollowing = false;

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



        if (aiData.currentTarget != null)
        {
            if (isFollowing == false)
            {
                isFollowing = true;
                animator.SetBool("isFollowing", isFollowing);
                StartCoroutine(Chase());
            }
        } else if (aiData.getTargetCount() > 0)
        {
            aiData.currentTarget = aiData.targets[0];
        }
    }

    private IEnumerator Chase()
    {
        if (aiData.currentTarget == null)
        {
            // Stop the agent
            Debug.Log("Stopping");
            movementInput = Vector2.zero;
            agent.MovementInput = movementInput;

            isFollowing = false;
            animator.SetBool("isFollowing", isFollowing);
            yield break;
        }
        else
        {
            float distance = Vector2.Distance(aiData.currentTarget.position, transform.position);

            if (distance < 1.5f)
            {
                //Debug.Log("Stopping");
                movementInput = Vector2.zero;
                agent.MovementInput = movementInput;

                isFollowing = false;
                animator.SetBool("isFollowing", isFollowing);
                yield break;
            }
            else
            {
                // Chase logic
                movementInput = movementDirectionSolver.GetDirectionToMove(steeringBehaviors, aiData);
                agent.MovementInput = movementInput;
                yield return new WaitForSeconds(aiUpdateDelay);
                StartCoroutine(Chase());
            }
        }
    }
}

