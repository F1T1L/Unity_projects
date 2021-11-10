using System.Collections;
using UnityEngine;

namespace RPG.Character
{
     [RequireComponent(typeof(Character))]
     [RequireComponent(typeof(WeaponSystem))]
    public class EnemyAI : MonoBehaviour
    {                  
        [SerializeField] float chaseRadius = 5f;
        [SerializeField] float waypointTolerance = 2f;
        //[SerializeField] float secondsBetweenShots = 2f;
        //[SerializeField] float damagePerShot = 10f;

        //[SerializeField] GameObject projectileToUse = null;
        //[SerializeField] GameObject projectileSocket = null;
        //[SerializeField] Vector3 aimOffset = new Vector3(0, 1.2f, 0);
        [SerializeField] WaypointContainer waypointContainer = null;
        enum State
        {
            normal, patrolling, attacking, chasing
        }
        State state = State.normal;       
        PlayerMovement player = null;        
        float attackRadius, distanceToPlayer;
        WeaponSystem weaponSystem;
        Character character;
        Vector3 nextWaypoint;
        int index=0;
        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();
            character = GetComponent<Character>();
        }
        private void Update()
        {           
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            weaponSystem = GetComponent<WeaponSystem>();
            attackRadius = weaponSystem.currentWeapon.GetMaxAttackRange();
            if (distanceToPlayer > chaseRadius && state != State.patrolling)
            {
                StopAllCoroutines();
                state = State.patrolling;
                StartCoroutine(Patrol());

            }
            if (distanceToPlayer <= chaseRadius && state!=State.chasing)
            {
                StopAllCoroutines();
                state = State.chasing;
                StartCoroutine(ChasePlayer());
                //aiCharacter.SetTarget(player.transform);     //todo delete old. 
                //aiCharacter.SetTarget(transform); //todo delete old.
            }

            if (distanceToPlayer <= attackRadius && state!=State.attacking)
            {
                StopAllCoroutines();
                LookAtPlayer();
                state = State.attacking;
                // InvokeRepeating("FireProjectile", 0f, secondsBetweenShots); // TODO switch to coroutines
            }

            if (distanceToPlayer > attackRadius)
            {                
               // CancelInvoke();
            }
        }
        IEnumerator Patrol()
        {
            while (true)
            {
                nextWaypoint = waypointContainer.transform.GetChild(index).position;
                character.SetDestination(nextWaypoint);
                if (Vector3.Distance(transform.position,nextWaypoint)<=waypointTolerance)
                {
                    index = (index + 1) % waypointContainer.transform.childCount;                    
                }
                yield return new WaitForSeconds(.3f);
            }           
        }
        IEnumerator ChasePlayer()
        {            
            while (distanceToPlayer >= attackRadius)
            {
                character.SetDestination(player.transform.position);
                yield return new WaitForEndOfFrame();
            }
        }

        private void LookAtPlayer()
        {
            Vector3 lookPos = player.transform.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
            transform.rotation = rotation;
        }
        //void FireProjectile()
        //{

        //    var projectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
        //    Projectile projectileComponent = projectile.GetComponent<Projectile>();
        //    projectileComponent.DamageCouse = damagePerShot;
        //    projectileComponent.SetShooter(gameObject);
        //    Vector3 vectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
        //    //Vector3 vectorToPlayer = (
        //    //    (new Vector3(player.transform.position.x, player.transform.position.y +
        //    //    capsulaPlayer.height * 0.7f, player.transform.position.z)) -
        //    //    projectileSocket.transform.position).normalized;

        //    var projectileSpeed = projectileComponent.SetDefaultLaunchSpeed();
        //    projectile.GetComponent<Rigidbody>().velocity = vectorToPlayer * projectileSpeed;
        //    // projectile.GetComponent<Rigidbody>().MovePosition(player.transform.position);
        //    // projectile.GetComponent<Rigidbody>().AddForce(vectorToPlayer * 5f);
        //    // projectile.transform.Translate(vectorToPlayer * projectileSpeed);
        //}            
        //void OnDrawGizmos()
        //{
        //    // Draw attack sphere 
        //    Gizmos.color = new Color(255f, 0, 0, .5f);
        //    Gizmos.DrawWireSphere(transform.position, attackRadius);

        //    // Draw chase sphere 
        //    Gizmos.color = new Color(0, 0, 255, .5f);
        //    Gizmos.DrawWireSphere(transform.position, chaseRadius);
        //}
    }
}