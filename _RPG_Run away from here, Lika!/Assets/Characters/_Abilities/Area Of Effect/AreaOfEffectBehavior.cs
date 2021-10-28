using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Character
{
    public class AreaOfEffectBehavior : AbilityBehavior
    {     
        public override void Use(AbilityUseParams aparams)
        {
            DoAoEDamage(aparams);
            PlayParticleEffect();
            PlayAbilitySound();
        }
        private void DoAoEDamage(AbilityUseParams aparams)
        {
            print("AreaOfEffect.USE()," +
                            " radius:" + (config as AreaOfEffect).GetRadius() +
                            " by " + gameObject.name +
                            ", BaseDamage:" + aparams.baseDamage +
                            ", eachTargetDmg: " + (config as AreaOfEffect).GetDamageToEachTarget());
            RaycastHit[] hits = Physics.SphereCastAll(
                this.transform.position,
                (config as AreaOfEffect).GetRadius(),
                Vector3.up,
                (config as AreaOfEffect).GetRadius()); ;
            foreach (var item in hits)
            {
                var damageAble = item.collider.gameObject.GetComponent<IDamageAble>();
                if (damageAble != null && item.collider.gameObject.layer == 7)
                {

                    damageAble.TakeDamage(aparams.baseDamage + (config as AreaOfEffect).GetDamageToEachTarget());
                }
            }
        }
    }
}