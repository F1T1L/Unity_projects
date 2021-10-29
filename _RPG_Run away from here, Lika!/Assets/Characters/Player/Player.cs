using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI;

namespace RPG.Character
{
    public class Player : MonoBehaviour
    {       
        
        [SerializeField] float basedamage = 10f;       
        [Range(0.0f,100.0f)] [SerializeField] float criticalChance =10f;       
        [SerializeField] float criticalHitMultiplier = 1.5f;  
        [SerializeField] ParticleSystem critHitParticle = null;
        [SerializeField] Weapon currentWeapon = null;        
        [SerializeField] AnimatorOverrideController animatorOverrideController = null;
        HealthSystem hpsystem;

        GameObject weaponObj;       
        Animator animator;
        SpecialAbilities specialAbilities;       
        CameraRaycaster cameraRaycaster;      
        float lastHitTime;       
        private void Start()
        {
            specialAbilities = GetComponent<SpecialAbilities>();
            hpsystem =GetComponent<HealthSystem>();
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;            
            PutWeaponInHand(currentWeapon);
            SetAttackAnimation();                                
        }
        public void PutWeaponInHand(Weapon weaponToUse)
        {
            currentWeapon = weaponToUse;
            var weapPrefab = weaponToUse.GetWeaponPrefab();
            GameObject weaponSocket = ReqestDominantHand();
            Destroy(weaponObj);
            weaponObj = Instantiate(weapPrefab, weaponSocket.transform);
            weaponObj.transform.localPosition = currentWeapon.gripTransform.localPosition;
            weaponObj.transform.localRotation = currentWeapon.gripTransform.localRotation;
            //Instantiate(weaponInUse, this.transform.Find("EthanRightHandThumb4"));
        }
      

        private void Update()
        {
            if (hpsystem.currentHealthPoints > Mathf.Epsilon)
            {
                ScanForAbilityKeyDown();
            }
        }

        private void ScanForAbilityKeyDown()
        {
            for (int i = 0; i < specialAbilities.GetNumberOfAbilities(); i++)
            {                
                if (Input.GetKeyDown((i+1).ToString()))
                {
                    specialAbilities.AttemptUseSpecialAbility(i);                    
                }
            }            
        }     
        private void SetAttackAnimation()
        {
            animator= GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController["Default attack"] = currentWeapon.GetAnimClip();
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
                AttackTarget();                
            } 
            else if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                specialAbilities.AttemptUseSpecialAbility(0);
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
            if (Time.time - lastHitTime >currentWeapon.GetHitDelay())
            {
                SetAttackAnimation();
                animator.SetTrigger("Attack");
                hpsystem.TakeDamage(CalculateDamage());
                lastHitTime = Time.time;
            }
        }

        private float CalculateDamage()
        {
            if (UnityEngine.Random.Range(0f, 100f) <= criticalChance)
            {
                print("CRITICAL_HIT! for <color=red>" +
                    ((basedamage + currentWeapon.GetBonusWeaponDmg()) * criticalHitMultiplier)+
                    "</color>");
                critHitParticle.Play();
                return (basedamage + currentWeapon.GetBonusWeaponDmg()) * criticalHitMultiplier;
            } else {
                print("normal_HIT! for <color=white>" + (basedamage + currentWeapon.GetBonusWeaponDmg())+"</color>");
                return basedamage + currentWeapon.GetBonusWeaponDmg();
            }  
        }

        private bool isTargetInRange(GameObject enemy)
        {
            return (Vector3.Distance(this.transform.position, enemy.transform.position)) <= currentWeapon.GetMaxAttackRange();
        }

        public void TakeDamage(float damage)
        {
            throw new NotImplementedException();
        }
    }            
}

//
//
//50%-Cursor 50hp,
// 100hp / 100 * (50%+x)
// 100hp / 100 *(50+25=75)
//


