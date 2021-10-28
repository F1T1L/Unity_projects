using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/AreaOfEffect"))]
    public class AreaOfEffect : AbilityConfig
    {        
        [SerializeField] float radius=5f,
                               damageToEachTarget=5f;
        public override AbilityBehavior GetBehaviorComponent(GameObject objectToAttachTo)
        {
            return objectToAttachTo.AddComponent<AreaOfEffectBehavior>();
        }
        public float GetDamageToEachTarget()
        {
            return damageToEachTarget;
        }
        public float GetRadius()
        {
            return radius;
        }       
    }
}