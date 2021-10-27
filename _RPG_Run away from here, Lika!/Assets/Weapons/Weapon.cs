using UnityEngine;

namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class Weapon : ScriptableObject
    {
        public Transform gripTransform;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;

        [SerializeField] float hitDelay = 1.5f;
        // [SerializeField] float minAttackRange= 2f;
        [SerializeField] float maxAttackRange = 2f;
        [SerializeField] float bonusWeaponDmg = 5f;
        public float GetHitDelay() { return hitDelay; }
        public float GetBonusWeaponDmg() { return bonusWeaponDmg; }
        public float GetMaxAttackRange() { return maxAttackRange; }
        public GameObject GetWeaponPrefab() { return weaponPrefab; }
        public AnimationClip GetAnimClip()
        {
            attackAnimation.events = new AnimationEvent[0];  //CLEAR ARRAY OF EVENTS. dont have Hit func atm.
            return attackAnimation;
        }
    }
}