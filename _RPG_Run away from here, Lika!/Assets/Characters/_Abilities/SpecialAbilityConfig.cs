using UnityEngine;

namespace RPG.Character
{    
    public abstract class SpecialAbilityConfig : ScriptableObject
    {       
        [Header("Special Ability")]
        [SerializeField] float energyCost = 10f;

        public abstract ISpecialAbility AddComponent(GameObject gameObjectToAttachTo);
    }
}
