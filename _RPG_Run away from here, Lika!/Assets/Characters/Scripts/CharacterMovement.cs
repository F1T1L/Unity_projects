using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;
using System;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]     
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] float moveThreshold = 1f;
        [SerializeField] float animSpeedMultiplier = 1f;
        [SerializeField] float movingTurnSpeed = 360;
        [SerializeField] float stationaryTurnSpeed = 180;
        [SerializeField] float moveSpeedMultiplier = .7f;
        [SerializeField] float stoppingDistance = 1f;        
        CameraRaycaster cameraRaycaster = null; 
        float turnAmount;
        float forwardAmount;       
        //GameObject walkTarget = null;       
        float h, v;
        NavMeshAgent navMeshAgent;
        Animator animator; 
        Rigidbody myRigidbody;
        void Start()
        {
            animator = GetComponent<Animator>();
            //animator.applyRootMotion = true;            
            myRigidbody = GetComponent<Rigidbody>();
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();                      
            //walkTarget = new GameObject("walkTarget");
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            cameraRaycaster.notifyOnMouseoverTerrainObservers += OnMouseoverTerrainObservers;            
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = true;
            navMeshAgent.stoppingDistance = stoppingDistance;
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