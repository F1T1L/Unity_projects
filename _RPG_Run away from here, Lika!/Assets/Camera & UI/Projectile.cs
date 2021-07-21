using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] public float projectileSpeed = 10f;
    public float DamageCouse;
    float timeToSelfDestroy;
    private void Start()
    {
        timeToSelfDestroy = Time.time;
    }
    private void Update()
    {        
        if (Time.time - timeToSelfDestroy >= 5f)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {        
        var damageable = other.gameObject.GetComponent(typeof(IDamageAble));
        if (damageable)
        {
      //  print("BALL TRIGGER: " + other.name );
            (damageable as IDamageAble).TakeDamage(DamageCouse);
        }
        Destroy(gameObject,0.05f);
    }
   
}
