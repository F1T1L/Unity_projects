using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
         gameObject.AddComponent<BoxCollider>().isTrigger=false;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        GameObject fx= Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
       // print("onParticleCollision(): Hit -> Enemy " +gameObject.name);
        Destroy(gameObject);
    }
}
