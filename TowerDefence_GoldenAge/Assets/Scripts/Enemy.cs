using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    IEnumerator FollowPath(List<Waypoint> path)
    {
        //float speed = 1.0f;
        //float step = speed * Time.deltaTime;
        foreach (Waypoint waypoint in path)
        {
            
            //transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, step);
            transform.position = waypoint.transform.position;
            if (transform.position != pf.startWaypoint.transform.position &&
                transform.position != pf.endWaypoint.transform.position)
            {
            waypoint.SetTopColor( waypoint.explored);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
