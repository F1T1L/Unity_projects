using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        ThirdPersonCharacter thirdPersonCharacter = null;   // A reference to the ThirdPersonCharacter on the object
        CameraRaycaster cameraRaycaster = null;
        // Vector3 clickPoint;
        AICharacterControl aiCharacterControl = null;
        GameObject walkTarget = null;
        // bool isInDirectMode = false;
        float h, v;
        //// TODO solve fight between serialize and const
        //[SerializeField] const int walkableLayerNumber = 6;
        //[SerializeField] const int enemyLayerNumber = 7;


        void Start()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            aiCharacterControl = GetComponent<AICharacterControl>();
            walkTarget = new GameObject("walkTarget");

            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            cameraRaycaster.notifyOnMouseoverTerrainObservers += OnMouseoverTerrainObservers;
           
           
        }

        void OnMouseoverEnemyObservers(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                aiCharacterControl.SetTarget(enemy.transform);                
            }
        }

        void OnMouseoverTerrainObservers(Vector3 destination) {
            if (Input.GetMouseButtonDown(0))
            {
                walkTarget.transform.position = destination;
                aiCharacterControl.SetTarget(walkTarget.transform);
            }
        }

        //void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
        //{
        //    switch (layerHit)
        //    {
        //        case enemyLayerNumber:
        //            // navigate to the enemy
        //            GameObject enemy = raycastHit.collider.gameObject;
        //            aiCharacterControl.SetTarget(enemy.transform);
        //            break;
        //        //case walkableLayerNumber:
        //        //    walkTarget.transform.position = raycastHit.point;
        //        //    aiCharacterControl.SetTarget(walkTarget.transform);

        //        //    break;
        //        default:
        //            Debug.LogWarning("Don't know how to handle mouse click for player movement");
        //            return;
        //    }
        //}

        // TODO make this get called again
        void ProcessDirectMovement()
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

            thirdPersonCharacter.Move(movement, false, false);
        }
    }
}