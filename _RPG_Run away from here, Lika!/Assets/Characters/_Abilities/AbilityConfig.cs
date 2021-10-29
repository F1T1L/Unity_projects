using UnityEngine;


namespace RPG.Character
{
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
       

        public void Use(GameObject gObj=null)
        {
            behavior.Use(gObj);            
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

