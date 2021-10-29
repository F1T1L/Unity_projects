using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] GameObject shooter;
        public float DamageCouse;
        float timeToSelfDestroy;

        public void SetShooter(GameObject shooter)
        {
            this.shooter = shooter;
        }

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
            var damageable = other.gameObject.GetComponent<HealthSystem>();
            //  if (damageable && other.transform.tag.Contains("Player"))
            if (damageable && (other.gameObject.layer != shooter.layer) && (damageable.gameObject!=null))
            {
                //  print("BALL TRIGGER: " + other.name );
                damageable.TakeDamage(DamageCouse);
            }
            Destroy(gameObject, 0.05f);
        }

        public float SetDefaultLaunchSpeed()
        {
            return projectileSpeed;
        }
    }
}