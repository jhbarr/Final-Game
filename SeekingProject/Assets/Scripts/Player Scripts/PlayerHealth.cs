using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public readonly int maximumHp = 5;
    public int currentHp;

    private void Start()
    {
        currentHp = maximumHp;
    }

    public void takeDamage(int n)
    {
        currentHp -= n;
        if (currentHp <= 0)
        {
            currentHp = 0;
        }
    }
}
