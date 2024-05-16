using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHealthManager : MonoBehaviour
{
    public GameObject Heart;

    Stack<GameObject> Hearts;

    [SerializeField] GameObject max;
    [SerializeField] GameObject canvas;


    PlayerHealth healthScript;

    // Start is called before the first frame update
    void Start()
    {
        Hearts = new();

        healthScript = max.GetComponent<PlayerHealth>();
        
        for (int i = 0; i < healthScript.maximumHp; i++)
        {
            
            Vector3 heartPos = Heart.transform.position;
            heartPos.x = Heart.transform.position.x + (128 + 25) * i;
            heartPos += new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Hearts.Push(Instantiate(Heart, heartPos, Heart.transform.rotation, transform));
        }


    }


    // Update is called once per frame
    void Update()
    {
        if (healthScript.currentHp < Hearts.Count)
        {
            Destroy(Hearts.Pop());
        }
    }
}
