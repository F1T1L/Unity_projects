using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;
using System;

namespace RPG.Character
{
    [SelectionBase]      
    public class Character : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField] RuntimeAnimatorController runtimeAnimatorController;
        [SerializeField] AnimatorOverrideController animOverController;
        [SerializeField] Avatar avatar;
        [SerializeField] [Range(.1f, 1f)] float animatorForwardCap = 1f;
        [Header("Capsule Collider")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0, 1.03f, 0);
        [SerializeField] float colliderRadius = 0.2f;
        [SerializeField] float colliderHeight = 2.03f;
        [Header("Movement")]
        [SerializeField] float moveThreshold = 1f;
        [SerializeField] float animSpeedMultiplier = 1f;
        [SerializeField] float movingTurnSpeed = 360;
        [SerializeField] float stationaryTurnSpeed = 180;
        [SerializeField] float moveSpeedMultiplier = .7f;        
        [Header("Nav Mesh Agent")]
        [SerializeField] float navMeshAgentSteeringSpeed = 1.0f;
        [SerializeField] float navMeshAgentStoppingDistance = 1.3f;
        [Header("Audio")]
        [SerializeField] float audioSourceSpatialBlend = 0.5f;
        CameraRaycaster cameraRaycaster = null; 
        float turnAmount;
        float forwardAmount;       
        //GameObject walkTarget = null;       
        float h, v;
        NavMeshAgent navMeshAgent;
        Animator animator; 
        Rigidbody myRigidbody; 
        bool isAlive = true;
        private void Awake()
        {
            AddRequeredComponents();
        }

        private void AddRequeredComponents()
        {
            
            animator =gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = runtimeAnimatorController;            
            animator.avatar = avatar;
            animator.applyRootMotion = true;

            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = colliderCenter;
            capsuleCollider.radius = colliderRadius;
            capsuleCollider.height = colliderHeight;

            myRigidbody = gameObject.AddComponent<Rigidbody>();
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = audioSourceSpatialBlend;

            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.speed = navMeshAgentSteeringSpeed;
            navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
            navMeshAgent.autoBraking = false;
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = true;
        }

        void Start()
        {   cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();                      
            //walkTarget = new GameObject("walkTarget");
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            cameraRaycaster.notifyOnMouseoverTerrainObservers += OnMouseoverTerrainObservers;          
          
        }
        private void Update()
        {
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                Move(navMeshAgent.desiredVelocity);
            }
            else { Move(Vector3.zero); }
        }

        internal void Kill()
        {
            print("dead");
        }

        public void Move(Vector3 move)
        {
            if (move.magnitude > moveThreshold)
            {
                move.Normalize();
            }
            var localMove = transform.InverseTransformDirection(move);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }
        void UpdateAnimator()
        {
            animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = animSpeedMultiplier;

        }
        void ApplyExtraTurnRotation()
        {
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }
        void OnAnimatorMove()
        {
            if (Time.deltaTime > 0)
            {
                Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
                velocity.y = myRigidbody.velocity.y;
                myRigidbody.velocity = velocity;
            }
        }
        void OnMouseoverEnemyObservers(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                navMeshAgent.SetDestination(enemy.transform.position);
               // aiCharacterControl.SetTarget(enemy.transform);                
            }
        }
        void OnMouseoverTerrainObservers(Vector3 destination) {
            if (Input.GetMouseButtonDown(0))
            {
                navMeshAgent.SetDestination(destination);
                //walkTarget.transform.position = destination;
                //aiCharacterControl.SetTarget(walkTarget.transform);
            }
        }
        // TODO make this get called again
        void ProcessDirectMovement()
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            Move(movement);
        }
     
    }
}