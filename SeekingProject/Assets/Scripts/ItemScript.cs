using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public string ItemName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject mm = GameObject.Find("MacroManager");
            mm.GetComponent<MacroManagerScript>().GetItem(gameObject);
        }
    }
}
