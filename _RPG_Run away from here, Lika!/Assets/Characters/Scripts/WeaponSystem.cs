using UnityEngine;
using UnityEngine.Assertions;

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
        float lastHitTime;
        void Start()
        {
            character = GetComponent<Character>();
            animator = GetComponent<Animator>();
            PutWeaponInHand(currentWeapon);
            SetAttackAnimation();
        }

        void Update()
        {

        }
        public void Attack(GameObject objToAttack) {
            target = objToAttack;
        }
        private void AttackTarget()
        {
            //Vector3 relative = transform.InverseTransformPoint(enemy.transform.position);                   
            //this.transform.Rotate(0, Mathf.Atan2(relative.x, relative.z)* Mathf.Rad2Deg, 0);
            if (Time.time - lastHitTime > currentWeapon.GetHitDelay())
            {
                SetAttackAnimation();
                animator.SetTrigger("Attack");
               // HealthSystem.TakeDamage(CalculateDamage());
                lastHitTime = Time.time;
            }
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