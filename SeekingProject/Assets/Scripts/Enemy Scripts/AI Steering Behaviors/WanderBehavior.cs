using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleAvoidanceBehavior;
using static UnityEngine.EventSystems.EventTrigger;

public class WanderBehavior : SteeringBehavior
{
    [SerializeField]
    private LayerMask obstacleLayerMask;

    [SerializeField]
    private float wanderRadius = 3f, targetReachedThreshhold = 1f;

    [SerializeField]
    private bool showGizmos = true;

    private Vector2 point;
    private Vector2 currentAngle;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        // If we are currently not moving towards any valid wander point, then we need to generate a new one
        if (aiData.wanderTarget == Vector2.zero)
        {
            // Until we find a valid point
            while (point == Vector2.zero)
            {
                currentAngle = (Random.insideUnitCircle * wanderRadius);
                Vector2 tempPoint = (Vector2)transform.position + currentAngle;
                Vector2 direction = (tempPoint - (Vector2)transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, wanderRadius, obstacleLayerMask);

                if (hit.collider == null)
                {
                    aiData.wanderTarget = tempPoint;
                    point = tempPoint;  
                }
                // Otherwise we need to look for another point
            }
        }

        // Check if we can reached the current wander target
        if (Vector2.Distance(transform.position, aiData.wanderTarget) < targetReachedThreshhold)
        {
            aiData.wanderTarget = Vector2.zero;
            point = Vector2.zero;
            return (danger, interest);
   
        }

        // If we have not reached the target, then steer towards it
        Vector2 directionToTarget = (aiData.wanderTarget - (Vector2)transform.position);
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
        return (danger, interest);


    }


    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        if (Application.isPlaying && point != Vector2.zero)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(point, 0.3f);
            
        }
    }
}
