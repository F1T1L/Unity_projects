using System.Collections;
using UnityEngine;

using RPG.CameraUI;

namespace RPG.Character
{
    public class PlayerMovement : MonoBehaviour
    {   
        Character character;
        SpecialAbilities specialAbilities;       
        CameraRaycaster cameraRaycaster;
        WeaponSystem weaponSystem;
           
        private void Start()
        {
            character = GetComponent<Character>();
            specialAbilities = GetComponent<SpecialAbilities>();
            weaponSystem = GetComponent<WeaponSystem>();         
            RegisterMouseEvents();
        }
        private void RegisterMouseEvents()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            cameraRaycaster.notifyOnMouseoverTerrainObservers += OnMouseoverTerrainObservers;
        }
        private void Update()
        {            
                ScanForAbilityKeyDown();           
        }   
        private void ScanForAbilityKeyDown()
        {
            for (int i = 0; i < specialAbilities.GetNumberOfAbilities(); i++)
            {                
                if (Input.GetKeyDown((i+1).ToString()))
                {
                    specialAbilities.AttemptUseSpecialAbility(i);                    
                }
            }            
        }   
        void OnMouseoverTerrainObservers(Vector3 dest)
        {
            if (Input.GetMouseButton(0))
            {
                character.SetDestination(dest);
            }
        }
        void OnMouseoverEnemyObservers(EnemyAI enemy)
        {            
            if ( Input.GetMouseButtonDown(0) && isTargetInRange(enemy.gameObject))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                weaponSystem.Attack(enemy.gameObject);                
            } 
            else if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(SmoothLerp(0.5f, enemy));
                specialAbilities.AttemptUseSpecialAbility(0,enemy.gameObject);
            }            
        }   
        private IEnumerator SmoothLerp(float time, EnemyAI enemy)
        {
            Vector3 vectorToTarget = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.x, vectorToTarget.z) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);  
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, q, (elapsedTime / time));                
                elapsedTime += Time.deltaTime;                          
                yield return null;
            }           
        } 
        private bool isTargetInRange(GameObject enemy)
        {
            return (Vector3.Distance(this.transform.position, enemy.transform.position)) <= weaponSystem.currentWeapon.GetMaxAttackRange();
        }      
    }            
}