using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/SelfHeal"))]
    public class SelfHeal : AbilityConfig
    {        
        [SerializeField] float amount=45f;            
        public float GetAmount()
        {
            return amount;
        }
        public override AbilityBehavior GetBehaviorComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<SelfHealBehavior>();
        }
    }
}