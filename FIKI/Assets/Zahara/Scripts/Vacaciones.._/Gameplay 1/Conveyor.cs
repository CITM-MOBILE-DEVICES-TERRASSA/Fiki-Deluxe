using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [FormerlySerializedAs("accelFrequency")] [SerializeField] private float accelDelay;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(accelDelay);
            speed += acceleration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = transform.right * speed;
        }
    }
}
