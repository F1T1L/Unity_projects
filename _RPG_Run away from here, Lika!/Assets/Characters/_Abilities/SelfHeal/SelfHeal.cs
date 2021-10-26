using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/SelfHeal"))]
    public class SelfHeal : AbilityConfig, ISpecialAbility
    {        
        [SerializeField] float amount=45f;
        public override void AddComponent(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<SelfHealBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }       
        public float GetAmount()
        {
            return amount;
        }
        public void Use()
        {
          
        }
    }
}