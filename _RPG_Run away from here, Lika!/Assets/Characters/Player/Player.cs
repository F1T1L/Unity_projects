using System;
using System.Collections;
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
        [Range(0.0f,100.0f)] [SerializeField] float criticalChance =10f;       
        [SerializeField] float criticalHitMultiplier = 1.5f;  
        [SerializeField] ParticleSystem critHitParticle = null;
        [SerializeField] Weapon weaponInUse = null;
        [SerializeField] AbilityConfig[] abilities;
        
        [SerializeField] AudioClip[] damageSounds=null;       
        [SerializeField] AudioClip[] deathSounds=null;       

        [SerializeField] AnimatorOverrideController animatorOverrideController = null;
        //[SerializeField] GameObject weaponSocket = null;

        AudioSource audioSource;
        Animator animator;
        Energy energyComponent;
        Enemy enemy = null;
        CameraRaycaster cameraRaycaster;      
        float lastHitTime;
        public float CurrentHealthPoints { get=>currentHealthPoints; set=>currentHealthPoints=value; }
        private void Start()
        {            
            audioSource = GetComponent<AudioSource>();
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            currentHealthPoints = maxHealthPoints;  
            PutWeaponInHand();
            SutupRuntimeAnimator();
            AttachAbilities();                      
        }

        private void AttachAbilities()
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].AddComponent(gameObject);                
            }
        }

        private void Update()
        {
            if (currentHealthPoints > Mathf.Epsilon)
            {
                ScanForAbilityKeyDown();
            }
        }

        private void ScanForAbilityKeyDown()
        {
            for (int i = 0; i < abilities.Length; i++)
            {                
                if (Input.GetKeyDown((i+1).ToString()))
                {
                    AttemptUseSpecialAbility(i);                    
                }
            }            
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
            this.enemy = enemy;
            if ( Input.GetMouseButtonDown(0) && isTargetInRange(enemy.gameObject))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                AttackTarget();                
            } 
            else if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                AttemptUseSpecialAbility(0);
            }            
        }

        private void AttemptUseSpecialAbility(int index)
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
        private void AttackTarget()
        {
            //Vector3 relative = transform.InverseTransformPoint(enemy.transform.position);                   
            //this.transform.Rotate(0, Mathf.Atan2(relative.x, relative.z)* Mathf.Rad2Deg, 0);
            if (Time.time - lastHitTime >weaponInUse.GetHitDelay())
            {
                animator.SetTrigger("Attack");
                enemy.TakeDamage(CalculateDamage());
                lastHitTime = Time.time;
            }
        }

        private float CalculateDamage()
        {
            if (UnityEngine.Random.Range(0f, 100f) <= criticalChance)
            {
                print("CRITICAL_HIT! for <color=red>" +
                    ((basedamage + weaponInUse.GetBonusWeaponDmg()) * criticalHitMultiplier)+
                    "</color>");
                critHitParticle.Play();
                return (basedamage + weaponInUse.GetBonusWeaponDmg()) * criticalHitMultiplier;
            } else {
                print("normal_HIT! for <color=white>" + (basedamage + weaponInUse.GetBonusWeaponDmg())+"</color>");
                return basedamage + weaponInUse.GetBonusWeaponDmg();
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
            CheckPlayerHealth();

        }

        private void CheckPlayerHealth()
        {
            if (currentHealthPoints <= 0) //Player Death!
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
            SceneManager.LoadScene(0);
        }
        /// <summary>Put <c>VALUE</c> AS % [float SiNGED number](with -/+, its important)</summary>
        public float HealthAsPercentage {
            get { return currentHealthPoints / maxHealthPoints; }
            set { currentHealthPoints = Mathf.Clamp(
                        currentHealthPoints + (maxHealthPoints / 100 * value), 0f, maxHealthPoints);                
            }
        }
    }            
}

//
//
//50%-Cursor 50hp,
// 100hp / 100 * (50%+x)
// 100hp / 100 *(50+25=75)
//


