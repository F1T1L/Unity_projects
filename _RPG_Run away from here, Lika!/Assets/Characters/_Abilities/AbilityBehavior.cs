using UnityEngine;
using System.Collections;
namespace RPG.Character
{

public abstract class AbilityBehavior : MonoBehaviour
{    
        protected AbilityConfig config;
        const string ATTACK_TRIGGER = "Attack";
        const string DEFAULT_ATTACK_STATE = "Default attack";
        const float DESTROY_TIMER = 20f;
        public abstract void Use(GameObject gObj=null);
        public void SetConfig(AbilityConfig conf)
        {
            config = conf;
        }
        protected void PlayParticleEffect()
        {            
            var prefabParticle = Instantiate(config.GetParticlePrefab(), this.transform);
            //prefabParticle.transform.parent = transform;  
            prefabParticle.transform.SetParent(transform);             
           // prefabParticle.GetComponent<ParticleSystem>().simulationSpace=ParticleSystemSimulationSpace.World;
            prefabParticle.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DestroyParticle(prefabParticle)); 
        }
        IEnumerator DestroyParticle(GameObject objPrefab)
        {
            while (objPrefab.GetComponent<ParticleSystem>().isPlaying)
            {
             yield return new WaitForSeconds(DESTROY_TIMER);
            }
            Destroy(objPrefab);        
            yield return new WaitForEndOfFrame();
        }
        protected void PlayAbilitySound()
        {
            GetComponent<AudioSource>().PlayOneShot(config.GetAudioClips()); 
        }
        protected void PlayAbilityAnimation()
        {
            var animatorOverrideController = GetComponent<Character>().GetAnimatorOverrideController();
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController[DEFAULT_ATTACK_STATE] = config.GetAbilityAnimation();
            animator.SetTrigger(ATTACK_TRIGGER);
        }
    }
}
