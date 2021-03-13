using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    Vector2Int gridPos;
    const int gridSize = 10;
    public int GetGridSize => gridSize;

    // Start is called before the first frame update
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
         Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
         Mathf.RoundToInt(transform.position.z / gridSize) * gridSize);
    }
    public void SetTopColor(Color color)
    {
     var mesh = transform.Find("Top").GetComponent<MeshRenderer>();
        mesh.material.color = color;
        
    }
}