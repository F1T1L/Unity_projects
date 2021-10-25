using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        [SerializeField] float basedamage = 10f;       
        [SerializeField] AudioClip[] damageSounds=null;       
        [SerializeField] AudioClip[] deathSounds=null;       

        [SerializeField] Weapon weaponInUse = null;
        [SerializeField] AnimatorOverrideController animatorOverrideController = null;
        //[SerializeField] GameObject weaponSocket = null;
        [SerializeField] SpecialAbility[] abilities;

        AudioSource audioSource;
        Animator animator;
        Energy energyComponent;
        CameraRaycaster cameraRaycaster;      
        float lastHitTime;
       
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            currentHealthPoints = maxHealthPoints;  
            PutWeaponInHand();
            SutupRuntimeAnimator();
            abilities[0].AddComponent(gameObject);            
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
                StartCoroutine(SmoothLerp(0.5f, enemy));
                AttackTarget(enemy);                
            } 
            else if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                AttemptUseSpecialAbility(0,enemy);
            }            
        }

        private void AttemptUseSpecialAbility(int index,Enemy enemy)
        {
            if (Time.time - lastHitTime > weaponInUse.GetHitDelay())
            {
                energyComponent = FindObjectOfType<Energy>();
                if (energyComponent.IsEnergyAvaible(abilities[index].GetEnergyCost()))// TODO script
                {
                    energyComponent.ConsumeEnergy(abilities[index].GetEnergyCost());
                    var abilityParams = new AbilityUseParams(enemy, basedamage);
                    abilities[index].Use(abilityParams);
                    animator.SetTrigger("AoE");
                }
                else { print("<color=orange><b>Need more Energy!</b></color>"); }
                lastHitTime = Time.time;
            }
        }
        private IEnumerator SmoothLerp(float time, Enemy enemy)
        {
            Vector3 vectorToTarget = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.x, vectorToTarget.z) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);  
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, q, (elapsedTime / time));                
                elapsedTime += Time.deltaTime;                          
                yield return null;
            }           
        }
        private void AttackTarget(Enemy enemy)
        {
            
            //Vector3 relative = transform.InverseTransformPoint(enemy.transform.position);                   
            //this.transform.Rotate(0, Mathf.Atan2(relative.x, relative.z)* Mathf.Rad2Deg, 0);
            

            if (Time.time - lastHitTime >weaponInUse.GetHitDelay())
                {
                    animator.SetTrigger("Attack");  
                    enemy.TakeDamage(basedamage);
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
            if (!audioSource.isPlaying)
            {
                audioSource.clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
                audioSource.PlayDelayed(0.05f);                
            }            
            if (currentHealthPoints<= 0) //Player Death!
            {                
                StartCoroutine(KillPlayer());                
            }
           
        }
        IEnumerator KillPlayer()
        {            
            AICharacterControl acc = GetComponent<AICharacterControl>();            
            acc.enabled = false;   // body after death will not move. turn off component.        
            animator.SetTrigger("DEATH_TRIGGER");
            //audioSource.Stop();
            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play();
            Debug.LogWarning("Player died.");
            yield return new WaitForSecondsRealtime(audioSource.clip.length);            
            SceneManager.LoadSceneAsync(0);
        }

        public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints; } }

    }
}