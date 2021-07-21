using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageAble
{

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float currentHealthPoints = 100f;

    [SerializeField] float attackRadius = 3f;
    [SerializeField] float chaseRadius = 5f;
    [SerializeField] float secondsBetweenShots = 2f;
    [SerializeField] float damagePerShot = 10f;

    [SerializeField] GameObject projectileToUse = null;
    [SerializeField] GameObject projectileSocket = null;
    [SerializeField] Vector3 aimOffset = new Vector3(0, 1.2f, 0);
    bool isAttacking = false;
    GameObject player = null;
    AICharacterControl aiCharacter = null;
    CapsuleCollider capsulaPlayer = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        capsulaPlayer = GetComponent<CapsuleCollider>();
        aiCharacter = GetComponent<AICharacterControl>();
        currentHealthPoints = maxHealthPoints;
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= chaseRadius)
        {
            aiCharacter.SetTarget(player.transform);
        }
        else
        {
            aiCharacter.SetTarget(transform);
        }

        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("FireProjectile", 0f, secondsBetweenShots); // TODO switch to coroutines
        }

        if (distanceToPlayer > attackRadius)
        {
            isAttacking = false;
            CancelInvoke();
        }
    }


    void FireProjectile()
    {

        var projectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.DamageCouse = damagePerShot;
        Vector3 vectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        //Vector3 vectorToPlayer = (
        //    (new Vector3(player.transform.position.x, player.transform.position.y +
        //    capsulaPlayer.height * 0.7f, player.transform.position.z)) -
        //    projectileSocket.transform.position).normalized;

        var projectileSpeed = projectileComponent.projectileSpeed;
        projectile.GetComponent<Rigidbody>().velocity = vectorToPlayer * projectileSpeed;
        // projectile.GetComponent<Rigidbody>().MovePosition(player.transform.position);
        // projectile.GetComponent<Rigidbody>().AddForce(vectorToPlayer * 5f);
        // projectile.transform.Translate(vectorToPlayer * projectileSpeed);
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if (currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    public float healthAsPercentage
    {
        get { return currentHealthPoints / (float)maxHealthPoints; }
    }
    void OnDrawGizmos()
    {
        // Draw attack sphere 
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        // Draw chase sphere 
        Gizmos.color = new Color(0, 0, 255, .5f);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
