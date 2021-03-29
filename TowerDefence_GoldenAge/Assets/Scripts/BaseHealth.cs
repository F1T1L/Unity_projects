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
    //void OnGUI()
    //{
    //    // Make a background box
    //    GUI.Box(new Rect(500 ,510, 400   , 300), "Loader Menu");

    //    // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
    //    if (GUI.Button(new Rect(500, 510, 155, 150), "Level 1"))
    //    {
    //        //Application.LoadLevel(1);
    //    }

    //    // Make the second button.
    //    if (GUI.Button(new Rect(750, 510, 155, 150), "Level 2"))
    //    {
    //      //  Application.LoadLevel(2);
    //    }
    //}
    private void GameOver()
    {
        print("GAME OVER!");
        Application.Quit();
    }

}
