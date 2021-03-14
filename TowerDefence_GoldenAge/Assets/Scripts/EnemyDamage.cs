using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints=10;
    [SerializeField] ParticleSystem hitPart;
    [SerializeField] ParticleSystem deathPart;
    ParticleSystem deathvfx;
  
    private void OnParticleCollision(GameObject other)
    {        
        ProcessHit();
        if (hitPoints <= 0)
        {
            deathvfx= Instantiate(deathPart,transform.GetChild(0).position,Quaternion.identity);
            deathvfx.Play();
            Destroy(deathvfx.gameObject, deathvfx.main.duration);
            KillEnemy();            
        }
    }  
    private void KillEnemy()
    {
        Destroy(gameObject);        
    }

    void ProcessHit()
    {
        hitPoints -= 1;
        hitPart.Play();

        //print("HP: " + hitPoints);    
    }
}

