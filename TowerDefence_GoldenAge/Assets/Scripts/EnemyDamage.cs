using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints=10;
    [SerializeField] ParticleSystem hitPart;
    [SerializeField] ParticleSystem deathPart;
    [SerializeField] AudioClip gotDamageSFX;
    [SerializeField] AudioClip DeathSFX;
    ParticleSystem deathvfx;
    AudioSource myAudioSource;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {        
        ProcessHit();
        if (hitPoints <= 0)
        {
            deathvfx= Instantiate(deathPart,transform.GetChild(0).position,Quaternion.identity);
            deathvfx.Play();
            Destroy(deathvfx.gameObject, deathvfx.main.duration);
            AudioSource.PlayClipAtPoint(DeathSFX,Camera.main.transform.position,0.05f);
            KillEnemy();            
        }
    }  
    private void KillEnemy()
    {              
        Destroy(gameObject);        
    }

    void ProcessHit()
    {
        AudioSource.PlayClipAtPoint(gotDamageSFX, Camera.main.transform.position, 0.05f);
        hitPoints -= 1;
        hitPart.Play();

        //print("HP: " + hitPoints);    
    }
}

