using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum Directions
    {
        Forward,
        Back,
        Left,
        Right
    };
    PathFinder pf;
    void Start()
    {
        pf = FindObjectOfType<PathFinder>();
        var path = pf.GetPath();
        StartCoroutine(FollowPath(path));
        
        //Waypoint[] wps= FindObjectsOfType<Waypoint>();        
        //foreach (var wp in wps)
        //{
        //    path.Add(wp.exploreFrom);            
        //}
        //Waypoint next = pf.endWaypoint;
        //path.Add(pf.endWaypoint);
        //while (next!=null)
        //{
        //    path.Add(next.exploreFrom);
        //    next = next.exploreFrom;         
        //    
        //}
        //foreach (var item in path)
        //{
        //    print(item);
        //}
        // path[0].SetTopColor(Color.red);
        // path[path.Count - 1].SetTopColor(Color.yellow);
        //       
    }
 

    private IEnumerator SmoothLerp(float time, Waypoint waypoint)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = waypoint.transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion finalRot = waypoint.transform.rotation;   
        
        var heading = transform.position - waypoint.transform.position;
        Directions direction = HeadingDirection(heading);
        //print("Direction: " + direction);               
        
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));                      
            transform.rotation = Quaternion.Lerp(startRot, SetRotationWhileMove(direction, finalRot,10f), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            transform.rotation.SetLookRotation(waypoint.transform.position);
            yield return null;
        }
      //  transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smooth * Time.deltaTime);
    }

    private Quaternion SetRotationWhileMove(Directions direction, Quaternion finalRot, float angle)
    {
        switch (direction)
        {
            case Directions.Forward:
                finalRot = Quaternion.AngleAxis(angle, new Vector3(1f, 0f, 0f));
                break;
            case Directions.Back:
                finalRot = Quaternion.AngleAxis(angle, new Vector3(-1f, 0f, 0f));
                break;
            case Directions.Left:
                finalRot = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, 1f));
                break;
            case Directions.Right:
                finalRot = Quaternion.AngleAxis(angle, new Vector3(0f, 0f, -1f));
                break;
            default:
                break;
        }
        return  finalRot;
    }

    private Directions HeadingDirection(Vector3 heading)
    {             
        if (heading.z <= -1f)
        {            
            return Directions.Forward;
        }
        if (heading.z >= 1f)
        {            
            return Directions.Back;
        }
        if (heading.x <= -1f)
        {           
            return Directions.Right;
        }
        if (heading.x >= 1f)
        {            
            return Directions.Left;
        }
        return Directions.Forward;
    }

   

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            StartCoroutine(SmoothLerp(1f, waypoint));
            //if (transform.position != pf.startWaypoint.transform.position &&
            //    transform.position != pf.endWaypoint.transform.position)
            //{
            //waypoint.SetTopColor( waypoint.explored);
            //}
            yield return new WaitForSeconds(1f);
        }
    }
    

}
