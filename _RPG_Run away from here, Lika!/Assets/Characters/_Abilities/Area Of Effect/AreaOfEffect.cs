using UnityEngine;
namespace RPG.Character
{
    [CreateAssetMenu(menuName = ("RPG/SpecialAbility/AreaOfEffect"))]
    public class AreaOfEffect : AbilityConfig, ISpecialAbility
    {        
        [SerializeField] float radius=5f,
                               damageToEachTarget=5f;
        public override void AddComponent(GameObject gameObjectToAttachTo)
        {
            var behaviorComponent = gameObjectToAttachTo.AddComponent<AreaOfEffectBehavior>();
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }
        public float GetDamageToEachTarget()
        {
            return damageToEachTarget;
        }
        public float GetRadius()
        {
            return radius;
        }
        public void Use()
        {
         
        }
    }
}