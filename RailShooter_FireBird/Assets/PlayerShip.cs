using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 40f;
    [Tooltip("in m or fields")] [SerializeField] float xMaximum = 10f;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 40f;
    [Tooltip("in m or fields")] [SerializeField] float yMaximum = 10f;
    [SerializeField] float rotateScale = -2.5f;
    [SerializeField] float controlPitch = -25f;
    [SerializeField] float controlYaw = 2f;
    [SerializeField] float controlRoll = -50f;
    

    float pitch, yaw, roll, xThrow, xOffset, rawX, xMax, yThrow, yOffset, rawY, yMax;
    // Update is called once per frame
    void Update()
    {
        MovingAxis();
        RotateAxis();
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
         xMax = Mathf.Clamp(rawX, -xMaximum, xMaximum);

         yThrow = Input.GetAxis("Vertical");
         yOffset = yThrow * ySpeed * Time.deltaTime;
         rawY = transform.localPosition.y + yOffset;
         yMax = Mathf.Clamp(rawY, -yMaximum, yMaximum);

        transform.localPosition = new Vector3(xMax, yMax, transform.localPosition.z);
    }
}
