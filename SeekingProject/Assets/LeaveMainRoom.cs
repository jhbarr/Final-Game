using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveMainRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject.Find("Max").transform.position = new Vector3(-13.72f, .78f, 0);
    }
}
