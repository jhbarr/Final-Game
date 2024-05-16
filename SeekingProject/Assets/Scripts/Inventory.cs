using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items;

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

    public void AddItem(string item)
    {
        items.Add(item);

        if (item.Equals("Sack Of Pebbles"))
        {
            canSack.SetActive(true);
        }

        if (item.Equals("Key"))
        {
            canKey.SetActive(true);
        }
    }

    public bool Contains(string itemName)
    {
        foreach (string item in items)
        {
            if (item.Equals(itemName))
            {
                return true;
            }
        }
        return false;
    }

    public void removeItem(string name)
    {
        items.Remove(name);

        if (name.Equals("Sack Of Pebbles"))
        {
            canSack.SetActive(false);
        }

        if (name.Equals("Key"))
        {
            canKey.SetActive(false);
        }
    }
}
