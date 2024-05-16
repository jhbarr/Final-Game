using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;

    public GameObject canSack;

    // Start is called before the first frame update
    void Start()
    {
        items = new();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(item.GetComponent<ItemScript>().ItemName);


        if (item.GetComponent<ItemScript>().ItemName.Equals("Sack Of Pebbles"))
        {
            canSack.SetActive(true);
        }
    }
}
