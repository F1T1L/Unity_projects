using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{     
    TextMesh textMesh;    
    Waypoint waypoint;
    int gridSize;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    private void Start()
    {
        textMesh = GetComponentInChildren<TextMesh>();
        gridSize = waypoint.GetGridSize;       
    }
    void Update()
    {
        SnapToGrid();
        UpdateLabel();

    }

    private void SnapToGrid()
    {        
        transform.position = new Vector3(waypoint.GetGridPos().x, 0f, waypoint.GetGridPos().y);
    }

    private void UpdateLabel()
    {
        string labelText = (waypoint.GetGridPos().x / gridSize) + "," + (waypoint.GetGridPos().y / gridSize);
        textMesh.text = labelText;
        gameObject.name = labelText + " Cube";
    }
}