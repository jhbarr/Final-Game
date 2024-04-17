using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleAvoidanceBehavior;

public class ContextBehavior : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = true;

    float[] interestGizmo = new float[0];
    Vector2 resultDirection = Vector2.zero;
    private float rayLength = 1;

    public void Start()
    {
        interestGizmo = new float[8];
    }

    // Get the vector that you want to steer towards
    public Vector2 GetDirectionToMove(List<SteeringBehavior> behaviors, AIData aiData)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

        // Loop through each behavior
        // ** I don't fully understand why this is run both in the EnemyAI class as well as this class
        foreach (SteeringBehavior behavior in behaviors)
        {
            (danger, interest) = behavior.GetSteering(danger, interest, aiData);
        }

        // Subtract danger value from interest array
        // This is how we heuristically decide which directions are not plausible for movement
        for (int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        interestGizmo = interest;

        // Get the average direction
        Vector2 outPutDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outPutDirection += Directions.eightDirections[i] * interest[i];
        }
        outPutDirection.Normalize();

        resultDirection = outPutDirection;

        // Return the resultant direction
        return resultDirection;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * rayLength);
        }
    }
}
