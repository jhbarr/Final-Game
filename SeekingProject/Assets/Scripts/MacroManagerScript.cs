using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MacroManagerScript : MonoBehaviour
{
    public GameObject max;

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void DropItem(Vector2 pos, GameObject item)
    {
        Debug.Log("Created " + item.GetComponent<ItemScript>().ItemName);
        GameObject newItem = Instantiate(item);
        newItem.transform.position = pos;
    }

    public void GetItem(GameObject item)
    {
        Debug.Log("Picked up " + item.GetComponent<ItemScript>().ItemName);
        max.GetComponent<Inventory>().AddItem(item);
        Destroy(item);
    }
}
