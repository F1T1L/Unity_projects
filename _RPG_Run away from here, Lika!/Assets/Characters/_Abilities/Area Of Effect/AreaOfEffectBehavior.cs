using UnityEngine;
using RPG.Core;
namespace RPG.Character
{
    public class AreaOfEffectBehavior : MonoBehaviour, ISpecialAbility
    {
        private AreaOfEffect config;
        
        public void SetConfig(AreaOfEffect config)
        {
           this.config = config;
        }
        void Start()
        {
            print("AreaOfEffect attached to " + gameObject.name);
        }
        public void Use(AbilityUseParams aparams)
        {
            print("AreaOfEffect.USE()," +
                " radius:" + config.GetRadius()+
                " by "+gameObject.name + 
                ", BaseDamage:" + aparams.baseDamage +
                ", eachTargetDmg: " +config.GetDamageToEachTarget());
            RaycastHit[] hits = Physics.SphereCastAll(
                this.transform.position,
                config.GetRadius(),
                Vector3.up,
                config.GetRadius()); ;
            foreach (var item in hits)
            {
                var damageAble = item.collider.gameObject.GetComponent<IDamageAble>();
                if (damageAble!=null && item.collider.gameObject.layer == 7)
                {
                    
                    damageAble.TakeDamage(aparams.baseDamage + config.GetDamageToEachTarget());
                }
            }                     
        }
    }
}