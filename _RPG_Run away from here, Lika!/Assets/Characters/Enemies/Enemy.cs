using System;
using UnityEngine;

namespace RPG.Character
{
    public class Enemy : MonoBehaviour
    {  

        [SerializeField] float attackRadius = 3f;
        [SerializeField] float chaseRadius = 5f;
        [SerializeField] float secondsBetweenShots = 2f;
        [SerializeField] float damagePerShot = 10f;

        [SerializeField] GameObject projectileToUse = null;
        [SerializeField] GameObject projectileSocket = null;
        [SerializeField] Vector3 aimOffset = new Vector3(0, 1.2f, 0);
        bool isAttacking = false;
        PlayerMovement player = null;
        HealthSystem hpsystem;
       

        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();                 
          
        }
        private void Update()
        {           
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= chaseRadius)
            {
                //aiCharacter.SetTarget(player.transform);               
            }
            else
            {
                //aiCharacter.SetTarget(transform);
            }

            if (distanceToPlayer <= attackRadius && !isAttacking)
            {
                isAttacking = true;
                LookAtPlayer();
                InvokeRepeating("FireProjectile", 0f, secondsBetweenShots); // TODO switch to coroutines
            }

            if (distanceToPlayer > attackRadius)
            {
                isAttacking = false;
                CancelInvoke();
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
        void FireProjectile()
        {

            var projectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
            Projectile projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.DamageCouse = damagePerShot;
            projectileComponent.SetShooter(gameObject);
            Vector3 vectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
            //Vector3 vectorToPlayer = (
            //    (new Vector3(player.transform.position.x, player.transform.position.y +
            //    capsulaPlayer.height * 0.7f, player.transform.position.z)) -
            //    projectileSocket.transform.position).normalized;

            var projectileSpeed = projectileComponent.SetDefaultLaunchSpeed();
            projectile.GetComponent<Rigidbody>().velocity = vectorToPlayer * projectileSpeed;
            // projectile.GetComponent<Rigidbody>().MovePosition(player.transform.position);
            // projectile.GetComponent<Rigidbody>().AddForce(vectorToPlayer * 5f);
            // projectile.transform.Translate(vectorToPlayer * projectileSpeed);
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

        public void TakeDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }
}