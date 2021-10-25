using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.Character
{
    [RequireComponent(typeof(Image))]
    public class PlayerHealthBar : MonoBehaviour
    {
        Image healthBarImage;
        Player player;
        void Start()
        {
            player = FindObjectOfType<Player>();
            healthBarImage = GetComponent<Image>();
        }
        void Update()
        {              
            healthBarImage.fillAmount = player.healthAsPercentage;
        }
    }
}