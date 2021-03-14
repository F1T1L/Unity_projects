using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] float spawnSeconds=3f;   


    private void Start()
    {        
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
           Instantiate(enemyObject, transform.position, Quaternion.identity, transform);
           yield return new WaitForSeconds(spawnSeconds);
        }       
    }

   
}
