using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitScript : MonoBehaviour
{
    public GameObject mm;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            mm.GetComponent<MacroManagerScript>().RestartScene();
        }
    }
}
