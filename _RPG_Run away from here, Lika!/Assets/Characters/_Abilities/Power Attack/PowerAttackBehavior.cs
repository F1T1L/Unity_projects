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
        public void Use(AbilityUseParams aparams)
        {
            DoDamage(aparams);
            PlayParticleEffect();
        }

        private void DoDamage(AbilityUseParams aparams)
        {
            print("PowerAttack.USE(), extra damage:" + config.GetExtraDamage() + " by " +
                gameObject.name + " BaseDamage:" + aparams.baseDamage);
            aparams.target.TakeDamage(aparams.baseDamage + config.GetExtraDamage());
        }

        private void PlayParticleEffect()
        {
            var prefab = Instantiate(config.GetParticlePrefab(), this.transform);
            ParticleSystem myParticleSystem = prefab.GetComponent<ParticleSystem>();
            myParticleSystem.Play();
            Destroy(prefab, myParticleSystem.main.duration);
        }
    }
}