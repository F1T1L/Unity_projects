using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Character
{
    public class SelfHealBehavior : MonoBehaviour, ISpecialAbility
    {
        private SelfHeal config;
        Player player=null;
        AudioSource audioSource=null;
        private void Start()
        {
            player = GetComponent<Player>();
            audioSource = GetComponent<AudioSource>();
        }
        public void SetConfig(SelfHeal config)
        {
           this.config = config;
        }        
        public void Use(AbilityUseParams aparams)
        {
            DoSelfHeal(aparams);
            PlayParticleEffect();
            audioSource.clip = config.GetAudioClip();
            audioSource.Play(); 
        }

        private void PlayParticleEffect()
        {
            var prefab = Instantiate(config.GetParticlePrefab(), this.transform);
            ParticleSystem myParticleSystem = prefab.GetComponent<ParticleSystem>();
            myParticleSystem.Play();
            Destroy(prefab, myParticleSystem.main.duration);
        }

        private void DoSelfHeal(AbilityUseParams aparams)
        {
            print(          "SelfHeal.USE()," +
                            " amount:" + config.GetAmount() +
                            " by " + gameObject.name);
            player = GetComponent<Player>();
            player.HealthAsPercentage = config.GetAmount();            
        }
     }
 }
