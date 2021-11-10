using UnityEngine;
namespace RPG.Character
{
    public class PowerAttackBehavior : AbilityBehavior
    {        
        public override void Use(GameObject gObj)
        {
            DoDamage(gObj);
            PlayParticleEffect();
            PlayAbilitySound();
            PlayAbilityAnimation();
        }      
        private void DoDamage(GameObject gObj)
        {
           // print("PowerAttack.USE(), extra damage:" + (config as PowerAttack).GetExtraDamage() + " by " +
           //     gameObject.name + " BaseDamage:" + gObj.baseDamage);
         gObj.GetComponent<HealthSystem>().TakeDamage((config as PowerAttack).GetExtraDamage());
        }       
    }
}