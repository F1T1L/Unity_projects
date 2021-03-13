using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    [SerializeField] Color startColor, endColor;
    
    Dictionary<Vector2Int, Waypoint> worldGrid = new Dictionary<Vector2Int, Waypoint>();
    Waypoint[] wps;
    void Start()
    {
        startWaypoint.SetTopColor(startColor);
        endWaypoint.SetTopColor(endColor);
        wps =FindObjectsOfType<Waypoint>();       
        foreach (var waypoint in wps)
        {           
            if (worldGrid.ContainsKey(waypoint.GetGridPos()))
            {
                Debug.LogError("Skipping Overplapping: " + waypoint.name);
            }
            else { 
            worldGrid.Add(waypoint.GetGridPos(), waypoint);                
            }
        }
        

    }   
}
