using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Character
{
    public class SelfHealBehavior : AbilityBehavior
    {
        HealthSystem hpSystem = null;
        private void Start()
        {
            hpSystem = GetComponent<HealthSystem>();            
        }
              
        public override void Use(AbilityUseParams aparams)
        {
            DoSelfHeal();
            PlayParticleEffect();
            PlayAbilitySound();
        }
        private void DoSelfHeal()
        {            
            print("SelfHeal.USE(),"+
                  " amount: + <color=green>"+
                  (config as SelfHeal).GetAmount()+
                  "</color>%, from current: <color=red>"+
                  hpSystem.currentHealthPoints+
                  "</color>");
            hpSystem.HealthAsPercentage = (config as SelfHeal).GetAmount();            
        }
     }
 }
