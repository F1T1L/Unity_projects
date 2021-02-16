using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField]  float period=2f;

    [SerializeField]  Vector3 moveSpeed = new Vector3(10f, 10f, 10f);

   // [SerializeField, Range(0,1)] 
    float movement;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSideWave = Mathf.Sin(cycles * tau);
        // Debug.Log(rawSideWave);
        movement = rawSideWave / 2f +0.5f;

      //  Vector3 temp = moveSpeed * movement;
        transform.position = startingPos + (moveSpeed * movement);
    }
}
