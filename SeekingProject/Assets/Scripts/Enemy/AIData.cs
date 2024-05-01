using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;

    public Transform currentTarget;

    public Vector2 wanderTarget;
    public float distanceToTarget = float.PositiveInfinity;

    public bool isFollowing = false;
    public bool isWandering = false;
    public bool isAttacking = false;

    public int getTargetCount() => targets == null ? 0 : targets.Count;
}
