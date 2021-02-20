using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionManager : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter()");
    }
    void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter()");
        Death();
    }

    void Death()
    {
        SendMessage("onPlayerDeath");
        //print("Death()");
    }
}
