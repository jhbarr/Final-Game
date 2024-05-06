using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Image Background;
    public Text GameOverText;
    public GameObject RestartButton;

    // Update is called once per frame
    void Update()
    {
        if (GameOverText.color.g < 0.5)
        {
            Color textCol = GameOverText.color;
            textCol.g += Time.deltaTime / 5;

            GameOverText.color = textCol;
        }
        else
        {
            Debug.Log(GameOverText.transform.position.y);
            if (GameOverText.transform.position.y < 700)
            {

                Vector3 textPos = GameOverText.transform.position;
                textPos.y += Time.deltaTime * 200;

                GameOverText.transform.position = textPos;
            }
            else
            {
                RestartButton.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        RestartButton.SetActive(false);

        Color textCol = GameOverText.color;
        textCol.g = 0;

        GameOverText.color = textCol;
    }
}
