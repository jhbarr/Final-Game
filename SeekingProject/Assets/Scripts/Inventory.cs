using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> items;

    public GameObject canSack;
    public GameObject canKey;

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
        string itemName = item.GetComponent<ItemScript>().ItemName;

        if (itemName.Equals("Sack Of Pebbles"))
        {
            canSack.SetActive(true);
        }

        if (itemName.Equals("Key"))
        {
            canKey.SetActive(true);
        }
    }

    public bool Contains(string itemName)
    {
        foreach (GameObject item in items)
        {
            if (itemName.Equals(item.name))
            {
                return true;
            }
        }
        return false;
    }
}
