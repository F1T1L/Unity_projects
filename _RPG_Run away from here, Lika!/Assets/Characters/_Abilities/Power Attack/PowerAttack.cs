using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/PowerAttack"))]
    public class PowerAttack : SpecialAbility, ISpecialAbility
    {
       // [Header("Power Attack Specific")]
        float extraDamage=10f;
        public override void AddComponent(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }
        public float GetExtraDamage()
        {
            return extraDamage;
        }      
        public void Use()
        {
         
        }
    }
}