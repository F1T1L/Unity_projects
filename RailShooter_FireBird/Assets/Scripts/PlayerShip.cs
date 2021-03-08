using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
<<<<<<< HEAD
{ 
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    //AC
=======
    //AC    
>>>>>>> parent of 636edbf (Merge branch 'main' of https://github.com/F1T1L/Unity_projects into main)
=======
    //AC    
>>>>>>> parent of 636edbf (Merge branch 'main' of https://github.com/F1T1L/Unity_projects into main)
=======
    //AC    
>>>>>>> parent of 636edbf (Merge branch 'main' of https://github.com/F1T1L/Unity_projects into main)
=======
{
>>>>>>> parent of 22fb52a (no message)
    [Header("General")]
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 40f;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 40f;
    [SerializeField] GameObject[] guns;
    [Header("Screen position")]
    [Tooltip("in m or fields")] [SerializeField] float yMaximumPosition = 10f;
    [Tooltip("in m or fields")] [SerializeField] float xMaximumPosition = 10f;
    [SerializeField] float rotateScale = -2.5f;
    [SerializeField] float controlPitch = -25f;
    [SerializeField] float controlYaw = 2f;
    [SerializeField] float controlRoll = -50f;

    bool isControllEnabled = true;
    float pitch, yaw, roll, xThrow, xOffset, rawX, xMax, yThrow, yOffset, rawY, yMax;
    
    void Update()
    {
        if (isControllEnabled)
        {
        MovingAxis();
        RotateAxis();
        Fire();
        }
    }

    private void Fire()
    {
        if (Input.GetButton("Fire"))
        {
            SetGunsActive(true);                     
        } else
        {
            SetGunsActive(false);                     
            
        }
    }

    private void SetGunsActive(bool v)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            //var em = guns[i].GetComponent<ParticleSystem>().emission;
            //em.enabled = v;
            guns[i].GetComponent<ParticleSystem>().enableEmission = v;            
        }
    }

    void onPlayerDeath() //вызов через сообщение, SendMessage.
    {
      //  print("onPlayerDeath()");
        isControllEnabled = false;
        
    }
    void RotateAxis()
    {
        pitch = transform.localPosition.y * rotateScale + yThrow * controlPitch;
        yaw = transform.localPosition.x * controlYaw;
        roll = xThrow * controlRoll;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void MovingAxis()
    {
         xThrow = Input.GetAxis("Horizontal");
         xOffset = xThrow * xSpeed * Time.deltaTime;
         rawX = transform.localPosition.x + xOffset;
         xMax = Mathf.Clamp(rawX, -xMaximumPosition, xMaximumPosition);

         yThrow = Input.GetAxis("Vertical");
         yOffset = yThrow * ySpeed * Time.deltaTime;
         rawY = transform.localPosition.y + yOffset;
         yMax = Mathf.Clamp(rawY, -yMaximumPosition, yMaximumPosition);

        transform.localPosition = new Vector3(xMax, yMax, transform.localPosition.z);
    }
    
}
