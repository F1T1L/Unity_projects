using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{    //v
    void Start()
    {
        Destroy(gameObject, 5f); 
    }   
}
