using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField]
    private float detectRadius = 2f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private bool showGizmos = true;

    Collider2D[] colliders;

    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, layerMask);
        aiData.obstacles = colliders;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
        {
            return;
        }
        if (Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D collider in colliders)
            {
                Gizmos.DrawSphere(collider.transform.position, 0.2f);
            }
        }
    }
}
