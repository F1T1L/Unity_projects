using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] int towerLimit=5;
    //Tower[] towers;
    Queue<Tower> que = new Queue<Tower>();
    public void AddTower(Waypoint wp)
    { 
        //towers = FindObjectsOfType<Tower>();        
        if (towerLimit > que.Count)
        {
           // print("Towers placed: " + (que.Count+1));
            var newTower= Instantiate(towerPrefab, wp.transform.position, Quaternion.identity);
            newTower.baseWaypoint = wp;
            wp.isPlaceble = false;
            que.Enqueue(newTower);
        }
        else
        {
            //print("Limit of Towers is " + towerLimit);
            MoveExistingTower(wp);
        }
        
    }

    private void MoveExistingTower(Waypoint wp)
    {
        var oldTower = que.Dequeue();
        oldTower.baseWaypoint.isPlaceble = true;
        wp.isPlaceble = false;
        oldTower.baseWaypoint = wp;
        oldTower.transform.position = wp.transform.position;
        que.Enqueue(oldTower);
    }
}

