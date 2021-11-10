using UnityEngine;

namespace RPG.Character
{
    public class WaypointContainer : MonoBehaviour
    {
       // [SerializeField] Transform[] waypoints;
        Vector3 firstPos, lastPos;
        private void Update()
        {
           // waypoints = GetComponentsInChildren<Transform>();
        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            firstPos = transform.GetChild(0).position;            
            lastPos = firstPos;
            foreach (Transform waypoint in transform)
            {                
                Gizmos.DrawSphere(waypoint.position, 0.2f);
                Gizmos.DrawLine(lastPos, waypoint.position);
                lastPos = waypoint.position;
            }
            Gizmos.DrawLine(lastPos,firstPos);

            //if (waypoints.Length < 2)
            //{
            //    return;
            //}
            //else
            //{
            //    Gizmos.color = new Color(255f, 0, 0, .8f);
            //    for (int i = 0; i < waypoints.Length - 1; i++)
            //    {
            //        Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
            //        Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            //    }
            //    Gizmos.DrawWireSphere(waypoints[waypoints.Length - 1].position, 0.2f);
            //    Gizmos.DrawLine(waypoints[waypoints.Length - 1].transform.position, waypoints[0].transform.position);
            //}
        }
    }
}
