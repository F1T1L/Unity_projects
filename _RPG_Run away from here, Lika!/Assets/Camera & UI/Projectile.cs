using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damageCause=1f;
    void OnTriggerEnter(Collider other)
    {
        var damageable = other.gameObject.GetComponent(typeof(IDamageAble));
        if (damageable)
        {
        print("BALL TRIGGER: " + other.name );
            (damageable as IDamageAble).TakeDamage(damageCause);
        }
    }
}
