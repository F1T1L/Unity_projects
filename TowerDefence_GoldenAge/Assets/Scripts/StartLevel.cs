using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{  
    void Awake()
    {
        Time.timeScale = 0;
    } 
    void OnGUI()
    {
        GUI.contentColor = Color.cyan;
        GUI.skin.box.fontSize = 110;
        GUI.skin.box.fontStyle = FontStyle.Bold;
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;        
        GUI.skin.button.fontSize = 120;
        GUI.skin.button.fontStyle = FontStyle.Bold;
        GUI.skin.button.border.left = 3;
        GUI.skin.button.border.right = 3;
        GUI.skin.button.border.top = Screen.width / 8;
        
         
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Place towers to defend your base!");
        if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 6, Screen.height / 1.5f, Screen.width / 3, Screen.height / 4), "Lets GO!"))
        {
            Time.timeScale = 1f;
            enabled = false;
            //Destroy(this);  //delete script from object :D          
        } 
    }
   
}
