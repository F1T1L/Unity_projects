using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RPG.CameraUI;
namespace RPG.Character
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] public float maxHealthPoints { get; set; } = 100f;
        [SerializeField] public float currentHealthPoints { get; set; }
       // [SerializeField] Image healthBar = null; NOT WORKING IN 2020 WTF?!
        [SerializeField] AudioClip[] damageSounds = null;
        [SerializeField] AudioClip[] deathSounds = null;
        [SerializeField] float deathVanishSeconds = 2.0f;

        /// <summary>SET:Put <c>VALUE</c> AS % [float SiNGED number](with -/+, its important)</summary>
        public float HealthAsPercentage
        {
            get { return currentHealthPoints / maxHealthPoints; }
            set
            {
                currentHealthPoints = Mathf.Clamp(
                      currentHealthPoints + (maxHealthPoints / 100 * value), 0f, maxHealthPoints);
            }
        }
        Animator animator;
        AudioSource audioSource;
        Character characterMovement;

        const string DEATH_TRIGGER = "DEATH_TRIGGER";
        void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            characterMovement = GetComponent<Character>();
            currentHealthPoints = maxHealthPoints;
        }
        void Update()
        {
           // UpdateHealthBar();
        }
        //void UpdateHealthBar()
        //{
        //     HealthAsPercentage;
            
        //}
        public void TakeDamage(float damage)
        {
            bool characterDies = (currentHealthPoints - damage <= 0);
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            var clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(clip);
            if (characterDies)
            {
                StartCoroutine(KillCharacter());
            }
        }
        IEnumerator KillCharacter()
        {
            characterMovement.Kill();
            characterMovement.enabled = false;// body after death will not move. turn off component. 
            animator.SetTrigger(DEATH_TRIGGER);

            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play(); // overrind any existing sounds
            Debug.LogWarning(gameObject.name+" died.");
            yield return new WaitForSecondsRealtime(audioSource.clip.length);

            var playerComponent = GetComponent<Player>();
            if (playerComponent && playerComponent.isActiveAndEnabled) // relying on lazy evaluation
            {
                SceneManager.LoadScene(0);
            }
            else // assume is enemy fr now, reconsider on other NPCs
            {
                Destroy(gameObject, deathVanishSeconds);
            }
        }


        /// <summary>add HP as number</summary>
        public void Heal(float points)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints + points, 0f, maxHealthPoints);
        }

    }
}
