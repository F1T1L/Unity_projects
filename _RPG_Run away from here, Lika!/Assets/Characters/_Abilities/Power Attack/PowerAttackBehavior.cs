using UnityEngine;
namespace RPG.Character
{
    public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility
    {
        private PowerAttack config;
        
        public void SetConfig(PowerAttack config)
        {
           this.config = config;
        }
        void Start()
        {
            print("PowerAttackBehavior attached to " + gameObject.name);
        }
        public void Use(AbilityUseParams aparams)
        {
            print("PowerAttack.USE(), extra damage:" + config.GetExtraDamage()+ " by "+
                gameObject.name + " BaseDamage:" + aparams.baseDamage);
            aparams.target.TakeDamage(aparams.baseDamage + config.GetExtraDamage());           
        }
    }
}