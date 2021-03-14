using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float attackRange =40f;

    Transform targetEnemy;
    
    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
        objectToPan.LookAt(targetEnemy);
        FireAtEnemy();
        }
        else{
            Shoot(false);                                   
        }
       
    }

    private void SetTargetEnemy()
    {
        if (targetEnemy && Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position) < attackRange)
        {
            return;
        }
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length==0)
        {
            return;
        }
        var closestEnemy = sceneEnemies[0].transform;
        foreach (var item in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, item.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform item)
    {
        if(Vector3.Distance(closestEnemy.transform.position, transform.position) >
        Vector3.Distance(item.transform.position, transform.position))
        {
            return item.transform;
        }
        return closestEnemy;
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
