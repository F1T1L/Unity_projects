using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("in ms^-1")] [SerializeField] float xSpeed = 40f;
    [Tooltip("in m or fields")] [SerializeField] float xMaximum = 10f;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 40f;
    [Tooltip("in m or fields")] [SerializeField] float yMaximum = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawX = transform.localPosition.x + xOffset;
        float xMax = Mathf.Clamp(rawX, -xMaximum, xMaximum);

        float yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawY = transform.localPosition.y + yOffset;
        float yMax = Mathf.Clamp(rawY, -yMaximum, yMaximum);

        transform.localPosition = new Vector3(xMax, yMax, transform.localPosition.z);

      

       


    }
}
