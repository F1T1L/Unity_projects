using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] float spawnSeconds=3f;
    [SerializeField] TextMesh textWaves;
    int count;
    private void Start()
    {        
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyObject, transform.position, Quaternion.identity, transform);
            count++;
            textWaves.text=count.ToString();
            yield return new WaitForSeconds(spawnSeconds);
        }       
    }   
}
