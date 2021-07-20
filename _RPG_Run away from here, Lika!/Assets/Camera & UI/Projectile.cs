using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] public float projectileSpeed = 10f;
    public float DamageCouse { get; set; }
    void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent(typeof(IDamageAble));
        if (damageable)
        {
        print("BALL TRIGGER: " + other.name );
            (damageable as IDamageAble).TakeDamage(DamageCouse);
        }
    }
}
