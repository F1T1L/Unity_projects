using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;
using System;

namespace RPG.Character
{
    public class Energy : MonoBehaviour
    {
       // [SerializeField] RawImage EnergyBar;
        RawImage EnergyBar;
        [SerializeField] float maxEnergyPoints=100f;
        [SerializeField] float pointsPerHit=10f;

        CameraRaycaster cameraRaycaster;
        float currentEnergyPoints;
        
        private void Start()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            EnergyBar = GetComponent<RawImage>();
            currentEnergyPoints = maxEnergyPoints;
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
        }

        private void OnMouseoverEnemyObservers(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var temp = currentEnergyPoints - pointsPerHit;
                currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
                float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
                EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
            }
           
        }        
    }
}