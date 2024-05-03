using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButtonScript : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

    }

    public void Restart()
    {
        Debug.Log("here");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
