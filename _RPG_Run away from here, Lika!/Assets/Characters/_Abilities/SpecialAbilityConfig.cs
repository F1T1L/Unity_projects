using UnityEngine;

namespace RPG.Character
{    
    public abstract class SpecialAbilityConfig : ScriptableObject
    {       
        [Header("Special Ability")]
        [SerializeField] float energyCost = 10f;
        protected ISpecialAbility behavior;
        public abstract void AddComponent(GameObject gameObjectToAttachTo);
    public void Use()
    {
            behavior.Use();
    }
    }
}
    
