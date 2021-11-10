using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;
namespace RPG.Character
{
[ExecuteInEditMode]
public class WeaponPickUpPoint : MonoBehaviour
{
        [SerializeReference] WeaponConfig weaponConfig;
        //[SerializeReference] AudioClip audioClip;
       // AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
           // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
        {
            if (!Application.isPlaying)
            {
            DestroyWeapon();
            SpawnWeapon();
            }
        }

        private void DestroyWeapon()
        {            
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        private void SpawnWeapon()
        {
            var weapon = weaponConfig.GetWeaponPrefab();
            weapon.transform.position = Vector3.zero;
            Instantiate(weapon, gameObject.transform);
        }
        private void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<WeaponSystem>().PutWeaponInHand(weaponConfig);           
            //audioSource.PlayOneShot(audioClip);
        }
    }
}
