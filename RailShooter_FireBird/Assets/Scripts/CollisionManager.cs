using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionManager : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] GameObject deathFX;
    void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter()");
    }
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter()");
        Death();
        deathFX.SetActive(true);//Включаем объект!
        Invoke("ReloadScene", levelLoadDelay);
    }
    void ReloadScene() // string reference, не трогай!
    {
        SceneManager.LoadScene(1);        
    }
    void Death()
    {
        SendMessage("onPlayerDeath");
        //print("Death()");
    }
}
