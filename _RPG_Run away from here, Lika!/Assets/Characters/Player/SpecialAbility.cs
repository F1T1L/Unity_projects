using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Character
{    
    public class SpecialAbility : MonoBehaviour
    {
        [SerializeField] float maxEnergyPoints = 100f;
        [SerializeField] float regenPointsPerSec = 10f;        
        Image EnergyBar = null;        
        float currentEnergyPoints;
        void Start()
        {            
            currentEnergyPoints = maxEnergyPoints;            
            EnergyBar = GetComponent<Image>();
            UpdateEnergyBar();
        }
        private void Update()
        {  
            EnergyRegeneration();
        }
        private void EnergyRegeneration()
        {
            if (currentEnergyPoints< maxEnergyPoints)
            {                
                currentEnergyPoints = Mathf.Clamp(
                    (currentEnergyPoints += regenPointsPerSec * Time.deltaTime), 0, maxEnergyPoints);
                UpdateEnergyBar();
            }            
        }     
        private void UpdateEnergyBar() {
            // float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
            // EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
            EnergyBar.fillAmount = (currentEnergyPoints / maxEnergyPoints);
        }
        public void ConsumeEnergy(float amount)
        {
            print("CurrentEnergy: <color=orange>" +  ((int)currentEnergyPoints) +
                  " </color>, spended: <color=red>" + amount + "</color>");         
            var temp = currentEnergyPoints - amount;
            currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
            UpdateEnergyBar();
        }        
        public bool IsEnergyAvaible(float amount)
        {            
            return (amount <= currentEnergyPoints);
        }
    }
}