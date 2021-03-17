using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] public Color explored;  
    Vector2Int gridPos;
    const int gridSize = 10;
    public bool isExplored = false;
    public bool isPlaceble = true;
    public Waypoint exploreFrom;
    public int GetGridSize => gridSize;
    Tower tw;
    void OnMouseOver()
    {  
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceble && (gameObject.tag!="Enemy"))
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
                isPlaceble = false;
            }else if(gameObject.tag == "Enemy")
            {
                print("We cant place there!");
            }            
        }       
    }
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
         Mathf.RoundToInt(transform.position.x / gridSize) ,
         Mathf.RoundToInt(transform.position.z / gridSize));
    }
    //public void SetTopColor(Color color)
    //{
    // var mesh = transform.Find("Top").GetComponent<MeshRenderer>();
    //     mesh.material.color = color;
        
    //}
    
}