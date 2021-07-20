using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble
{
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float currentHealthPoints= 100f;
    
    void IDamageAble.TakeDamage(float damage) {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage,0f,maxHealthPoints);
    }
    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints ; } }

}
