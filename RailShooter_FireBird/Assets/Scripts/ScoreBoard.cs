using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{   
    int score;
    TextMesh textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "Score:";
    }
    
   
    public void ScoreHit(int number)
    {
        score += number;
        textMesh.text = "Score: \n"+ score.ToString();
    }
}
