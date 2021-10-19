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
    public abstract class SpecialAbility : ScriptableObject
    {
        [Header("Special Ability")]
        [SerializeField] float energyCost = 10f;
        protected ISpecialAbility behavior;
        public abstract void AddComponent(GameObject gameObjectToAttachTo);
        public void Use(AbilityUseParams aparams)
        {
            behavior.Use(aparams);
        }
        public float GetEnergyCost()
        {
            return energyCost;
        }
    }
    public interface ISpecialAbility
    {
        void Use(AbilityUseParams aparams);
    }
}

