using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/PowerAttack"))]
    public class PowerAttack : SpecialAbilityConfig, ISpecialAbility
    {
       // [Header("Power Attack Specific")]
        [SerializeField] float extraDamage=10f;
        public override ISpecialAbility AddComponent(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
            behaviorComponent.SetConfig(this);
            return behaviorComponent;
        }

        public void Use()
        {
         
        }
    }
}