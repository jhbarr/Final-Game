using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownScript : MonoBehaviour
{
    public Image biteBar;
    public Image jumpBar;

    [SerializeField] GameObject max;

    Max_Script max_script;

    private float bitePercent;
    private float jumpPercent;


    // Start is called before the first frame update
    void Start()
    {
        max_script = max.GetComponent<Max_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (max_script.biteCounter > 0)
        {
            bitePercent = 1;
        }
        else
        {
            bitePercent = max_script.biteCdCount / max_script.biteCooldown;
        }
        biteBar.fillAmount = 1 - bitePercent;

        if (max_script.jumping)
        {
            jumpPercent = 1;
        }
        else
        {
            jumpPercent = max_script.jumpCdCount / max_script.jumpCooldown;
        }
        jumpBar.fillAmount = 1 - jumpPercent;


    }
}
