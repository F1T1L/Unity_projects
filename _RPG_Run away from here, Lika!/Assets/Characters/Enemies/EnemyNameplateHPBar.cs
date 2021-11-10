using UnityEngine;
using UnityEngine.UI;
using RPG.Character;

namespace RPG.CameraUI
{
    public class EnemyNameplateHPBar : MonoBehaviour
    {
        RawImage image;
        HealthSystem healthSystem;
        private void Start()
        {
            image = GetComponent<RawImage>();
            healthSystem = GetComponentInParent<HealthSystem>();
        }
        private void Update()
        {
            float xValue = -(healthSystem.HealthAsPercentage / 2f) - 0.5f;
            image.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
            
        }
    }
}