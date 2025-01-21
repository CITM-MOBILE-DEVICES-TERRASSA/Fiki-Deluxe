using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball;
    public float platformHeight = 0.8f;
    private float targetY; 
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero; 
    private Coroutine moveCoroutine; 

    void Start()
    {
        targetY = 1.74f; // Initial Y position for platform 0
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    void Update()
    {
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("Ball").transform;
        }
        float ballY = ball.position.y;
        float currentPlatform = Mathf.Floor(ballY / platformHeight);
        float platformBaseY = currentPlatform * platformHeight;
        float newTargetY = 1.74f + platformBaseY;

        if (ballY >= platformBaseY + 0.2f && ballY <= platformBaseY + 0.6f && Mathf.Abs(newTargetY - targetY) > Mathf.Epsilon)
        {
            targetY = newTargetY;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(MoveCameraToTargetY());
        }
    }

    IEnumerator MoveCameraToTargetY()
    {
        while (Mathf.Abs(transform.position.y - targetY) > Mathf.Epsilon)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            yield return null;
        }
    }
}
