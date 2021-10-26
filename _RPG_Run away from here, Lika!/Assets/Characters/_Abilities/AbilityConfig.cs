using UnityEngine;
using RPG.Core;

namespace RPG.Character
{
    public struct AbilityUseParams
    {
        public IDamageAble target;
        public float baseDamage;
        public AbilityUseParams(IDamageAble target, float baseDamage)
        {
            this.target = target;
            this.baseDamage = baseDamage;
        }
    }
    public abstract class AbilityConfig : ScriptableObject
    {
        [Header("Special Ability")]
        [SerializeField] float energyCost = 10f;
        [SerializeField] GameObject particlePrefab= null;
        [SerializeField] AudioClip audioClip= null;
        protected ISpecialAbility behavior;
        public abstract void AddComponent(GameObject gameObjectToAttachTo);
        public void Use(AbilityUseParams aparams)
        {
            behavior.Use(aparams);
            //AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
        }
        public float GetEnergyCost()
        {
            return energyCost;
        }
        public GameObject GetParticlePrefab()
        {
            return particlePrefab;
        }
        public AudioClip GetAudioClip()
        {
            return audioClip;
        }

    }
    public interface ISpecialAbility
    {
        void Use(AbilityUseParams aparams);
    }
}

