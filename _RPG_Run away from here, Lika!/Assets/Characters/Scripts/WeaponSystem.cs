using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

namespace RPG.Character
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] float basedamage = 10f;
        [Range(0.0f, 100.0f)] [SerializeField] float criticalChance = 10f;
        [SerializeField] float criticalHitMultiplier = 1.5f;
        [SerializeField] ParticleSystem critHitParticle = null;
        [SerializeField] public WeaponConfig currentWeapon = null;
        Character character;
        Animator animator; 
        GameObject weaponObj,target;
        float lastHitTime, hitDelay;
        bool targetIsDead, targetIsOutOfRange;
        void Start()
        {
            character = GetComponent<Character>();
            animator = GetComponent<Animator>();
            PutWeaponInHand(currentWeapon);
            SetAttackAnimation();
        }
        void Update()
        {
            if (target==null)
            {
                targetIsDead = false;
                targetIsOutOfRange = false;
            }
            else
            {
                var targetHealth = target.GetComponent<HealthSystem>().HealthAsPercentage;
                targetIsDead = targetHealth <= Mathf.Epsilon;
                var distanceToTarget=Vector3.Distance(transform.position, target.transform.position);
                targetIsOutOfRange = distanceToTarget > currentWeapon.GetMaxAttackRange();
            }
            if ((character.GetComponent<HealthSystem>().HealthAsPercentage<=Mathf.Epsilon) ||
                targetIsDead || targetIsOutOfRange)
            {                
                StopAllCoroutines();
            }
        }
        public void Attack(GameObject objToAttack)
        { 
            target = objToAttack;             
            StartCoroutine(AutoAttack());       
        }

        internal void StopAttacking()
        {
            StopAllCoroutines();
        }

        IEnumerator AutoAttack()
        {
            bool attackerIsAlive = GetComponent<HealthSystem>().HealthAsPercentage >= Mathf.Epsilon;
            bool targetIsAlive = target.GetComponent<HealthSystem>().HealthAsPercentage >= Mathf.Epsilon;
            while (attackerIsAlive && targetIsAlive)
            {
                hitDelay=currentWeapon.GetHitDelay()*character.GetAnimSpeedMultiplier();
                if (Time.time - lastHitTime > hitDelay)
                {
                    AttackAnimation();
                    lastHitTime = Time.time;
                }
                yield return new WaitForSeconds(hitDelay);
            }
        }  
        private void AttackAnimation()
        {
            //Vector3 relative = transform.InverseTransformPoint(enemy.transform.position);                   
            //this.transform.Rotate(0, Mathf.Atan2(relative.x, relative.z)* Mathf.Rad2Deg, 0);
            transform.LookAt(target.transform);
            SetAttackAnimation();
            animator.SetTrigger("Attack");
            StartCoroutine(DamageDelay(hitDelay));
        }
        IEnumerator DamageDelay(float hitDelay)
        {
            yield return new WaitForSecondsRealtime(hitDelay/3);
            target.GetComponent<HealthSystem>().TakeDamage(CalculateDamage());
        }
        private float CalculateDamage()
        {
            if (UnityEngine.Random.Range(0f, 100f) <= criticalChance)
            {
                print("CRITICAL_HIT! for <color=red>" +
                    ((basedamage + currentWeapon.GetBonusWeaponDmg()) * criticalHitMultiplier) +
                    "</color>");
                critHitParticle.Play();
                return (basedamage + currentWeapon.GetBonusWeaponDmg()) * criticalHitMultiplier;
            }
            else
            {
                print("normal_HIT! for <color=white>" + (basedamage + currentWeapon.GetBonusWeaponDmg()) + "</color>");
                return basedamage + currentWeapon.GetBonusWeaponDmg();
            }
        }
        private GameObject ReqestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            Assert.IsFalse(dominantHands.Length <= 0, "No DominantHand found on Player, add one.");
            Assert.IsFalse(dominantHands.Length > 1, "More then 1 DominantHand.scripts, remove some.");
            return dominantHands[0].gameObject;
        }
        private void SetAttackAnimation()
        {
            var animatorOverrideController = character.GetAnimatorOverrideController();
            animator.runtimeAnimatorController = animatorOverrideController;
           animatorOverrideController["Default attack"] = currentWeapon.GetAnimClip();
        }
        public void PutWeaponInHand(WeaponConfig weaponToUse)
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

    }
}