using UnityEngine;
using System.Collections;
namespace RPG.Character
{

public abstract class AbilityBehavior : MonoBehaviour
{    
        protected AbilityConfig config;
        const float DESTROY_TIMER = 20f;
        public abstract void Use(AbilityUseParams aparams);
        public void SetConfig(AbilityConfig conf)
        {
            config = conf;
        }
        protected void PlayParticleEffect()
        {            
            var prefabParticle = Instantiate(config.GetParticlePrefab(), this.transform);
            prefabParticle.transform.parent = transform;
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
    }
}
