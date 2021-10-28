using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/PowerAttack"))]
    public class PowerAttack : AbilityConfig
    {
        // [Header("Power Attack Specific")]
        float extraDamage= 10f;
        public override AbilityBehavior GetBehaviorComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<PowerAttackBehavior>();
        }
        public float GetExtraDamage()
        {
            return extraDamage;
        } 
    }
}