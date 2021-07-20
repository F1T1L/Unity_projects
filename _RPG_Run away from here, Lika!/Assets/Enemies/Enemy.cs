using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageAble
{
    
    [SerializeField] float maxHealthPoints=100f;
    [SerializeField] float currentHealthPoints=100f;
    [SerializeField] float radius=10f;
   
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
    void IDamageAble.TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }
    public float healthAsPercentage
    {
        get { return currentHealthPoints / (float)maxHealthPoints; }
    }
}
