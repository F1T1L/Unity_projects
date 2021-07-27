using UnityEngine;

namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class Weapon : ScriptableObject
    {
        public Transform gripTransform;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        public GameObject GetWeaponPrefab() { return weaponPrefab; }
        public AnimationClip GetAnimClip()
        {
            attackAnimation.events = new AnimationEvent[0];  //CLEAR ARRAY OF EVENTS. dont have Hit func atm.
            return attackAnimation;
        }
    }
}