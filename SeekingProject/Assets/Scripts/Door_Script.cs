using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Script : MonoBehaviour
{
    public Animator animator;
    public GameObject max;
    public AudioSource creak;

    public GameObject block;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory iv = max.GetComponent<Inventory>();
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (iv.Contains("Key"))
            {
                animator.SetBool("IsOpening", true);
                iv.removeItem("Key");
                creak.Play();
                block.SetActive(false);
            }
        }
        
    }
}
