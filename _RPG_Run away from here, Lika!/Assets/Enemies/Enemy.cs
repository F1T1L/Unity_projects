using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] float maxHealthPoints=100f;
    [SerializeField] float radius=10f;
    float currentHealthPoints;
    GameObject player = null;
    AICharacterControl aiCharacter = null;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacter=GetComponent<AICharacterControl>();
    }
    private void Update()
    {   
        if ( Vector3.Distance(player.transform.position,transform.position) <= radius )
        {
            aiCharacter.SetTarget(player.transform);
            //  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, radius);
        }
        else
        {
            aiCharacter.target = null;
        }
    }
    public float healthAsPercentage
    {
        get { return currentHealthPoints / (float)maxHealthPoints; }
    }
}
