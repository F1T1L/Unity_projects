using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character;

namespace RPG.CameraUI
{
public class HealthBar : MonoBehaviour
{
        Image image;
        Player player;
        private void Start()
        {
            image = GetComponent<Image>();   
            player = FindObjectOfType<Player>();
        }
        private void Update()
        {
            image.fillAmount=player.GetComponent<HealthSystem>().HealthAsPercentage;
        }
    }
}
