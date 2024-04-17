using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ObstacleAvoidanceBehavior;

public class SeekBehavior : SteeringBehavior
{
    // How close the enemy wants to get to the player or the player's last position before it stops
    [SerializeField]
    private float targetReachedThreshhold = 2f;

    [SerializeField]
    private bool showGizmos = true;

    bool reachedLastTarget = true;

    private Vector2 targetPositionCached;
    private float[] interestsTemp;


    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        // If we don't have a target stop seeking
        // Otherwise set a new target
        if (reachedLastTarget)
        {
            if (aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                // Select the target that's closest to the enemy
                aiData.currentTarget = aiData.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        // cache the last position only if we still see the target (if the targets collection is not empty)
        // If we still see the player and if the player is the target that we are currently pursuing
        // If the player disappears, then we'll need to look for something else
        // ** Remember that if we don't see the player, then it won't be in targets
        if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
        {
            targetPositionCached = aiData.currentTarget.position;
        }

        // First check if we have reached the target
        //Debug.Log(Vector2.Distance(transform.position, targetPositionCached));
        //Debug.Log(Vector2.Distance(transform.position, targetPositionCached) < targetReachedThreshhold);
        if (Vector2.Distance(transform.position, targetPositionCached) < targetReachedThreshhold)
        {
            Debug.Log("target Reached");
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        // *** If we haven't reached the target do the main logic of this steering class
        Vector2 directionToTarget = (targetPositionCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionToTarget.normalized, Directions.eightDirections[i]);

            // Only accept directions that are less than 90 degrees to the target direction
            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }

        interestsTemp = interest;
        return (danger, interest);
    }


    public void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawSphere(targetPositionCached, 0.2f);
        if (Application.isPlaying && interestsTemp != null)
        {
            if (interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
                if (reachedLastTarget == false)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(targetPositionCached, 0.1f);
                }
            }
        }
    }
}
