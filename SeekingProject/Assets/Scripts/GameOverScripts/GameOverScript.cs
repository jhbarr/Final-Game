using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Image Background;
    public Text GameOverText;
    public GameObject RestartButton;


    public float displayDelay;
    private float displayCounter;

    // Update is called once per frame
    void Update()
    {
        if (displayCounter > 0)
        {
            displayCounter -= Time.deltaTime;
        }
        else
        {

            if (Background.color.a < 1)
            {
                Color bgCol = Background.color;
                bgCol.a += Time.deltaTime;
                Background.color = bgCol;
            }
            else
            {
                if (GameOverText.color.g < 0.5)
                {
                    Color textCol = GameOverText.color;
                    textCol.g += Time.deltaTime / 5;
                    textCol.a = 1;
                    GameOverText.color = textCol;
                }
                else
                {
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
        }
    }

    private void OnEnable()
    {
        GameObject.Find("MacroManager").GetComponent<AudioSource>().Stop();

        RestartButton.SetActive(false);
        displayCounter = displayDelay;

        Color textCol = GameOverText.color;
        textCol.g = 0;
        textCol.a = 0;
        GameOverText.color = textCol;

        Color bgCol = Background.color;
        bgCol.a = 0;
        Background.color = bgCol;
    }
}
