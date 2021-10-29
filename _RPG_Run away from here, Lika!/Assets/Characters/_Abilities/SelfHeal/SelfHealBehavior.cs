using UnityEngine;
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
              
        public override void Use(GameObject gObj )
        {            
            DoSelfHeal(gObj);
            PlayParticleEffect();
            PlayAbilitySound();
        }
        private void DoSelfHeal(GameObject gObj)
        {            
            print("SelfHeal.USE(),"+
                  " amount: + <color=green>"+
                  (config as SelfHeal).GetAmount()+
                  "</color>%, from current: <color=red>"+
                  hpSystem.currentHealthPoints+
                  "</color> for +<color=green>"+
                  +(int)hpSystem.maxHealthPoints / 100 * (config as SelfHeal).GetAmount()+
                  "</color> HP!");
            // hpSystem.HealthAsPercentage = (config as SelfHeal).GetAmount();            
            gObj.GetComponent<HealthSystem>().HealthAsPercentage = (config as SelfHeal).GetAmount();
            
        }
     }
 }
