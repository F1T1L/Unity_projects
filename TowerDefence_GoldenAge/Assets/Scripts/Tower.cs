using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float attackRange =40f;

    
    void Update()
    {
        if (targetEnemy)
        {
        objectToPan.LookAt(targetEnemy);
        FireAtEnemy();
        }
        else{
            Shoot(false);           
        }
    }

    private void FireAtEnemy()
    {
        float distance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distance <= attackRange)
        {
            Shoot(true);
        }
        else { 
            Shoot(false);
        }
    }

    private void Shoot(bool value)
    {
        projectileParticle.gameObject.SetActive(value);
        
    }
}
