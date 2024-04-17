using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{ 

    [SerializeField]
    private Agent agent;

    private Vector2 direction;

    //private void Start()
    //{
       
    //}

    private void Update()
    {
        agent.MovementInput = GetDirectionToMove();
        Debug.Log(direction);
    }

    private Vector2 GetDirectionToMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        

        return direction.normalized;
    }
}
