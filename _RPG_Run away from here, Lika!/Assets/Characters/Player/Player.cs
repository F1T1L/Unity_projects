using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI;
using RPG.Core;
using RPG.Weapons;

namespace RPG.Character
{
    public class Player : MonoBehaviour, IDamageAble
    {
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] float currentHealthPoints = 100f;

        [SerializeField] float damagePerHit = 10f;       

        [SerializeField] Weapon weaponInUse = null;
        [SerializeField] AnimatorOverrideController animatorOverrideController = null;
        //[SerializeField] GameObject weaponSocket = null;
        [SerializeField] SpecialAbilityConfig ability1;

        Animator animator;

        CameraRaycaster cameraRaycaster;
        float distanceToEnemy;
        float lastHitTime;
       
        private void Start()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            currentHealthPoints = maxHealthPoints;
            PutWeaponInHand();
            SutupRuntimeAnimator();
            ability1.AddComponent(gameObject);

        }

        private void SutupRuntimeAnimator()
        {
            animator= GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController["Default attack"] = weaponInUse.GetAnimClip();
        }

        private void PutWeaponInHand()
        {
            GameObject weaponSocket = ReqestDominantHand();
            //var weaponPrefab = weaponInUse.GetWeaponPrefab();       
            var weapon = Instantiate(weaponInUse.GetWeaponPrefab(), weaponSocket.transform);
            weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
            weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
            //Instantiate(weaponInUse, this.transform.Find("EthanRightHandThumb4"));
        }

        private GameObject ReqestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            Assert.IsFalse(dominantHands.Length <= 0, "No DominantHand found on Player, add one.");
            Assert.IsFalse(dominantHands.Length > 1, "More then 1 DominantHand.scripts, remove some.");
            return dominantHands[0].gameObject;
        }

        void OnMouseoverEnemyObservers(Enemy enemy)
        {
            if ( Input.GetMouseButtonDown(0) && isTargetInRange(enemy.gameObject))
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.transform.position), Time.deltaTime);
                AttackTarget(enemy);                 
            } 
            if (Input.GetMouseButtonDown(1))
            {
                AttemptUseSpecialAbility(enemy);
            }            
        }

        private void AttemptUseSpecialAbility(Enemy enemy)
        {
            var getEnergyComponent = GetComponents<Energy>();
            foreach (var item in getEnergyComponent)
            {
                item.ConsumeEnergy(10f);
            }

            //if (getEnergyComponent.IsEnergyAvaible(10f))// TODO script
            //{
            //    getEnergyComponent.ConsumeEnergy(10f);
            //}

        }

            private void AttackTarget(Enemy enemy)
        {                
                if (Time.time - lastHitTime >weaponInUse.GetHitDelay())
                {
                    animator.SetTrigger("Attack");  
                    enemy.TakeDamage(damagePerHit);
                    lastHitTime = Time.time;
                }           
        }

        private bool isTargetInRange(GameObject enemy)
        {
            return (Vector3.Distance(this.transform.position, enemy.transform.position)) <= weaponInUse.GetMaxAttackRange();
        }

        void IDamageAble.TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        }
        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    }
}