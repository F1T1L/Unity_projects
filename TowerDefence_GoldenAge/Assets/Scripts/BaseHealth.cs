using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseHealth : MonoBehaviour
{
    [SerializeField] int baseHealth = 20;
    [SerializeField] int decreaseHealth = 1;
    [SerializeField] TextMesh textHealth;
    private void Start()
    {
        textHealth.text = baseHealth.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (baseHealth>0)
        {
            baseHealth-= decreaseHealth;
            textHealth.text = baseHealth.ToString();
        } else {
            GameOver();
        }
    }

    private void GameOver()
    {
        print("GAME OVER!");        
    }

}
