using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] int scorePerHit = 10;
    int score;
    TextMesh textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = "Score:";
    }
    
    //void Update()
    //{
    //    Score();
    //}
    public void ScoreHit()
    {
        score += scorePerHit;
        textMesh.text = score.ToString();
    }
}
