using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;   

    void Start()
    {        
       // path[0].SetTopColor(Color.red);
       // path[path.Count - 1].SetTopColor(Color.yellow);
        StartCoroutine(FollowPath());       
    }
    
    IEnumerator FollowPath()
    {        
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);        
        }        
    }
}
