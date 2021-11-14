using UnityEngine;
namespace RPG.Character
{
    public class AudioTrigger : MonoBehaviour
    {
        [SerializeField] AudioClip clip;
       // [SerializeField] int layerFilter = 0;
        [SerializeField] float triggerRadius = 5f;
        [SerializeField] bool isOneTimeOnly = true;

        [SerializeField] bool hasPlayed = false;
        AudioSource audioSource;
        PlayerControl player;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = clip;
            player = FindObjectOfType<PlayerControl>();
        }
        private void Update()
        {
            if (Vector3.Distance(transform.position, player.transform.position)<=triggerRadius)
            {
                RequestPlayAudioClip();
            }
        }
        void RequestPlayAudioClip()
        {
            if (isOneTimeOnly && hasPlayed)
            {
                return;
            }
            else if (audioSource.isPlaying == false)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 255f, 0, .5f);
            Gizmos.DrawWireSphere(transform.position, triggerRadius);
        }
    } }