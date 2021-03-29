using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 20;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (baseHealth>0)
        {
            --baseHealth;
        } else {
            GameOver();
        }
    }

    private void GameOver()
    {
        print("GAME OVER!");        
    }

}
