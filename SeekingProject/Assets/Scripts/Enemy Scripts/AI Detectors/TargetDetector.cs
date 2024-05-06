using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField]
    private float targetDetectionRange = 4f;

    [SerializeField]
    private LayerMask obstacleLayerMask, playerLayerMask;

    [SerializeField]
    private bool showGizmos = true;

    public override void Detect(AIData aiData)
    {

        List<Transform> colliders = null;

        // Find if the player is near
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            // Check if the enemy can see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstacleLayerMask);

            // Make sure that the collider we see is on the player layer mask level
            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.Log("Something is seen");
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
        }
        aiData.targets = colliders;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (showGizmos == false)
    //        return;

    //    Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

    //    if (colliders == null)
    //        return;
    //    Gizmos.color = Color.magenta;
    //    foreach (var item in colliders)
    //    {
    //        Gizmos.DrawSphere(item.position, 0.3f);
    //    }
    //}
}