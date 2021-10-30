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
        PlayerMovement player;
        private void Start()
        {
            image = GetComponent<Image>();   
            player = FindObjectOfType<PlayerMovement>();
        }
        private void Update()
        {
            image.fillAmount=player.GetComponent<HealthSystem>().HealthAsPercentage;
        }
    }
}
