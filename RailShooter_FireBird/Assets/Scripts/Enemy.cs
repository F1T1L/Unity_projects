using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;

    ScoreBoard scoreBoard;
    void Start()
    {
         gameObject.AddComponent<BoxCollider>().isTrigger=false;
         scoreBoard = FindObjectOfType<ScoreBoard>();         
    }
    
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        GameObject fx= Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
       // print("onParticleCollision(): Hit -> Enemy " +gameObject.name);
        Destroy(gameObject);
    }
}
