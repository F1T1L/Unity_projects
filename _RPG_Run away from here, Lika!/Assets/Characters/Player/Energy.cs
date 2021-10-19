using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Character
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] float maxEnergyPoints = 100f;
        [SerializeField] float pointsPerHit = 10f;

        RawImage EnergyBar = null;
        //CameraRaycaster cameraRaycaster;
        float currentEnergyPoints;
        private void Start()
        {
            currentEnergyPoints = maxEnergyPoints;
            //cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            //cameraRaycaster.notifyOnMouseoverEnemyObservers += OnMouseoverEnemyObservers;
            EnergyBar = GetComponent<RawImage>();
        }

        //void OnMouseoverEnemyObservers(Enemy enemy)
        //{
        //    if (Input.GetMouseButtonDown(3))
        //    {
        //        var temp = currentEnergyPoints - pointsPerHit;
        //        currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
        //        float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
        //        this.EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        //    }
        //}

        public void ConsumeEnergy(float amount)
        {
            print("CurrentEnergy: <color=orange>" +  currentEnergyPoints +
                  " </color>, cost: <color=red>" + amount + "</color>");           
            //  currentEnergyPoints = Mathf.Clamp((currentEnergyPoints - amount), 0, maxEnergyPoints);                
            //  EnergyBar.uvRect = new Rect(-((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f, 0f, 0.5f, 1f);
            var temp = currentEnergyPoints - amount;
            currentEnergyPoints = Mathf.Clamp(temp, 0, maxEnergyPoints);
            float xValue = -((currentEnergyPoints / maxEnergyPoints) / 2f) - 0.5f;
            EnergyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
        public bool IsEnergyAvaible(float amount)
        {            
            return (amount <= currentEnergyPoints);
        }
    }
}