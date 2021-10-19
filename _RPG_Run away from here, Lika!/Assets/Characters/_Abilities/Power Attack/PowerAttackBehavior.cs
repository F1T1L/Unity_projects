using UnityEngine;
namespace RPG.Character
{
    public class PowerAttackBehavior : MonoBehaviour, ISpecialAbility
    {
        private PowerAttack config;
        
        public void SetConfig(PowerAttack config)
        {
           this.config = config;
        }

        public void Use()
        {
            print("PowerAttack USE()");
        }
    }
}