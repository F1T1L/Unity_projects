using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Character
{    
    public class SpecialAbilities : MonoBehaviour
    {
        [SerializeField] AbilityConfig[] abilities;
        [SerializeField] public float maxEnergyPoints { get; set; } = 100f;
        [SerializeField] public float regenPointsPerSec { get; set; } = 10f;        
        Image EnergyBar;        
        AudioSource audioSource;
        float currentEnergyPoints;
        public float CurrentEnergyPoints {
            get=> currentEnergyPoints;
            set {currentEnergyPoints = Mathf.Clamp(currentEnergyPoints+ value, 0f, maxEnergyPoints);}
        }        

        /// <summary>SET:Put <c>VALUE</c> AS % [float SiNGED number](with -/+, its important)</summary>
        public float EnergyAsPercentage
        {
            get { return currentEnergyPoints / maxEnergyPoints; }
            set
            {
                currentEnergyPoints = Mathf.Clamp(
                      currentEnergyPoints + (maxEnergyPoints / 100 * value), 0f, maxEnergyPoints);
            }
        }
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentEnergyPoints = maxEnergyPoints;            
            EnergyBar = GetComponent<Image>();
            UpdateEnergyBar();
            AttachAbilities();
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
            if (EnergyBar)
            {
            EnergyBar.fillAmount = (currentEnergyPoints / maxEnergyPoints);

            }
            // float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
            // EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
        private void AttachAbilities()
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].AttachAbility(gameObject);
            }
        }
        public void AttemptUseSpecialAbility(int index, GameObject Obj=null)
        {
            
            if (IsEnergyAvaible(abilities[index].GetEnergyCost()))
            {
                ConsumeEnergy(abilities[index].GetEnergyCost());
                //var abilityParams = new AbilityUseParams(1);
                // TODO ability.use
                

                abilities[index].Use(gameObject);
                
                //animator.SetTrigger("AoE");
            }
            else { print("<color=orange><b>Need more Energy!</b></color>"); }

        }
        public int GetNumberOfAbilities()
        {
            return abilities.Length;
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