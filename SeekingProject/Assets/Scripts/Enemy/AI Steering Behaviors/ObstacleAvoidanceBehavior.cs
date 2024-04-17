using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceBehavior : SteeringBehavior
{
    [SerializeField]
    private float radius = 2f, agentColliderSize = 0.16f;

    [SerializeField]
    private bool showGizmos = true;

    float[] dangersResultsTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData)
    {
        foreach (Collider2D obstacleCollider in aIData.obstacles)
        {
            Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            // Calculate weight based on distance to the obstacle
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (radius - distanceToObstacle) / radius; // Will be between 0 and 1

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            // Add obstacle parameters to the danger array
            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                // Dot product is between 0 if they are perpendicular and 1 if they are the same direction
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]); 

                // The final value is the percentage away that the direction is from the direction to the obstacle
                // And how far away the enemy is from an obstacle
                // ** So the value will be higher if it points further away from the obstacle
                float valueToPutIn = result * weight;

                // Override the value if the value is higher than the current one
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultsTemp = danger;
        return (danger, interest);
    }

    public static class Directions
    {
        public static List<Vector2> eightDirections = new List<Vector2>
        {
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized,
        };
    }

    public void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;

        if (Application.isPlaying && dangersResultsTemp != null)
        {
            if (dangersResultsTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangersResultsTemp[i]);
                }
            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
