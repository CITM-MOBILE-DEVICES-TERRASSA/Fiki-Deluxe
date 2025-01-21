using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public float growthSpeed = 0.1f;
    public Vector3 maxScale = new Vector3(0.5f, 0.5f, 0.5f); 
    public GameObject ball; 
    public float thresholdDistance = 2.0f; 
    public float rotationSpeed = 2.0f;
    public bool isGrounded = false;
    public bool isNear = false;
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);

        if (distanceToBall < thresholdDistance)
        {
            if (transform.localScale.x < maxScale.x && transform.localScale.y < maxScale.y && transform.localScale.z < maxScale.z)
            {
                transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
                isNear = true;
            }
        }
        if (isNear)
        {
            transform.RotateAround(transform.parent.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (transform.position.y <= 0.5) 
        {
            Destroy(gameObject);
        }
    }
}
