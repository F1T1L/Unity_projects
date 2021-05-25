using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxHealthPoints = 100f;
    float currentHealthPoints;
    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints ; } }

}
