using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Character
{
    public class Energy : MonoBehaviour
    {
        RawImage EnergyBar=null;
        [SerializeField] float maxEnergyPoints=100f;
      
        float currentEnergyPoints;
        [SerializeField] float pointsPerHit = 10f;
        CameraRaycaster cameraRaycaster;
        private void Start()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            EnergyBar = GetComponent<RawImage>();
            currentEnergyPoints = maxEnergyPoints;            
        }
        
        void OnMouseoverEnemyObservers(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(3))
            {
                var temp = currentEnergyPoints - pointsPerHit;
                currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
                float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
                EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
            }
        }
          
        public void ConsumeEnergy(float amount)
        {
            print("ConsumeEnergy " + amount);
            //  currentEnergyPoints = Mathf.Clamp((currentEnergyPoints - amount), 0, maxEnergyPoints);                
            //  EnergyBar.uvRect = new Rect(-((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f, 0f, 0.5f, 1f);
            var temp = currentEnergyPoints - pointsPerHit;
            currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
            float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
            EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }    
        public bool IsEnergyAvaible(float amount)
        {
            print("currentEnergy: " + currentEnergyPoints + " asked: " + amount);
                return amount <= currentEnergyPoints;
        }
    }
}