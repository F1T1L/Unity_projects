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
        transform.position = new Vector3(waypoint.GetGridPos().x * gridSize, 0f, waypoint.GetGridPos().y * gridSize);
    }

    private void UpdateLabel()
    {
        string labelText = (waypoint.GetGridPos().x ) + "," + (waypoint.GetGridPos().y );
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}