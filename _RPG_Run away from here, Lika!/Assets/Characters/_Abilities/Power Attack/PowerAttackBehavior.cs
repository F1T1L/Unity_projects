using UnityEngine;
namespace RPG.Character
{
    public class PowerAttackBehavior : AbilityBehavior
    {        
        public override void Use(AbilityUseParams aparams)
        {
            DoDamage(aparams);
            PlayParticleEffect();
            PlayAbilitySound();
        }      
        private void DoDamage(AbilityUseParams aparams)
        {
            print("PowerAttack.USE(), extra damage:" + (config as PowerAttack).GetExtraDamage() + " by " +
                gameObject.name + " BaseDamage:" + aparams.baseDamage);
         //   aparams.target.(aparams.baseDamage + (config as PowerAttack).GetExtraDamage());
        }       
    }
}