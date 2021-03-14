using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] public Waypoint startWaypoint, endWaypoint;
    [SerializeField] Color startColor, endColor;
    
    Dictionary<Vector2Int, Waypoint> worldGrid = new Dictionary<Vector2Int, Waypoint>();
    Waypoint[] wps;
    Queue<Waypoint> que = new Queue<Waypoint>();
    public bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();
    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
        LoadBlocks();
        PathFind();
        CreatePath(); 
        }
        return path;
    }

    private void LoadBlocks()
    {
        SetStartAndEndColors();
        wps = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in wps)
        {
            if (worldGrid.ContainsKey(waypoint.GetGridPos()))
            {
                Debug.LogError("Skipping Overplapping: " + waypoint.name);
            }
            else
            {
                worldGrid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }

    private void SetStartAndEndColors()
    {
        startWaypoint.SetTopColor(startColor);
        endWaypoint.SetTopColor(endColor);
    }

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };
    //void Start()
    //{
    //    startWaypoint.SetTopColor(startColor);
    //    endWaypoint.SetTopColor(endColor);
    //    wps =FindObjectsOfType<Waypoint>();       
    //    foreach (var waypoint in wps)
    //    {           
    //        if (worldGrid.ContainsKey(waypoint.GetGridPos()))
    //        {
    //            Debug.LogError("Skipping Overplapping: " + waypoint.name);
    //        }
    //        else { 
    //        worldGrid.Add(waypoint.GetGridPos(), waypoint);                
    //        }
    //    }        
    //    PathFind();
    //    CreatePath();
    //}

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint next = endWaypoint.exploreFrom;
        while (next !=startWaypoint)
        {
            path.Add(next);
            next = next.exploreFrom;

        }
        path.Add(startWaypoint);
        path.Reverse();
    }

    private void PathFind()
    {
        que.Enqueue(startWaypoint);
        while (que.Count >0 && isRunning)
        {
            searchCenter = que.Dequeue();
            searchCenter.isExplored = true;
            HoldIfEndFoud();
            ExploreAround();
            
        }
    }

    private void HoldIfEndFoud()
    {

        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreAround()
    {
        if (!isRunning)  { return;  }

        foreach (var dir in directions)
        {
            Vector2Int explore = searchCenter.GetGridPos() + dir;
            if (worldGrid.ContainsKey(explore) && !worldGrid[explore].isExplored && !que.Contains(worldGrid[explore]))
            {               
                que.Enqueue(worldGrid[explore]);
                worldGrid[explore].exploreFrom = searchCenter;
            }

        }
    }
}
