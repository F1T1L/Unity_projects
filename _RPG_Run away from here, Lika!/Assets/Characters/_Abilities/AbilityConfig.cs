using UnityEngine;
using RPG.Core;

namespace RPG.Character
{
    public struct AbilityUseParams
    {
        
        public float baseDamage;
        public AbilityUseParams( float baseDamage)
        {
            
            this.baseDamage = baseDamage;
        }
    }
    public abstract class AbilityConfig : ScriptableObject
    {
        [Header("Special Ability")]
        [SerializeField] float energyCost = 10f;
        [SerializeField] GameObject particlePrefab= null;
        [SerializeField] AudioClip[] audioClips= null;
        protected AbilityBehavior behavior;
        public void AttachAbility(GameObject objectToAttachTo) {

            AbilityBehavior behaviorComponent = GetBehaviorComponent(objectToAttachTo);
            behaviorComponent.SetConfig(this);
            behavior = behaviorComponent;
        }

        public abstract AbilityBehavior GetBehaviorComponent(GameObject objectToAttachTo);
       

        public void Use(AbilityUseParams aparams)
        {
            behavior.Use(aparams);            
        }
        public float GetEnergyCost()
        {
            return energyCost;
        }
        public GameObject GetParticlePrefab()
        {
            return particlePrefab;
        }
        public AudioClip GetAudioClips()
        {
            return audioClips[Random.Range(0,audioClips.Length)];
        }

    }    
}

