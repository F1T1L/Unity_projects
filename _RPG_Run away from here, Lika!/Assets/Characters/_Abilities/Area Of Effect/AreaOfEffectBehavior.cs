using UnityEngine;
using System;

namespace RPG.Character
{
    public class AreaOfEffectBehavior : AbilityBehavior
    {     
        public override void Use(GameObject gObj)
        {
            DoAoEDamage();
            PlayParticleEffect();
            PlayAbilitySound();
        }
        private void DoAoEDamage( )
        {
            print("AreaOfEffect.USE()," +
                            " radius:" + (config as AreaOfEffect).GetRadius() +
                            " by " + gameObject.name +
                            ", eachTargetDmg: " + (config as AreaOfEffect).GetDamageToEachTarget());
            RaycastHit[] hits = Physics.SphereCastAll(
                this.transform.position,
                (config as AreaOfEffect).GetRadius(),
                Vector3.up,
                (config as AreaOfEffect).GetRadius()); ;
            foreach (var item in hits)
            {
                var damageAble = item.collider.gameObject.GetComponent<HealthSystem>();
                if (damageAble != null && item.collider.gameObject.layer == 7)
                {

                    damageAble.TakeDamage( (config as AreaOfEffect).GetDamageToEachTarget());
                }
            }
        }
    }
}