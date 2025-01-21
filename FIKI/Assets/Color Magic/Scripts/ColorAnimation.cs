using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAnimation : MonoBehaviour
{

    public float speed = 5f;
    public Vector3 direction = Vector3.right;
    public float distance = 10f;

    private Vector3 startPos;
    private bool isMoving = true;

    void Start()
    {

        startPos = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {

            transform.Translate(direction.normalized * speed * Time.deltaTime);


            if (Vector3.Distance(startPos, transform.position) >= distance)
            {

                isMoving = false;
                transform.position = startPos + direction.normalized * distance;
            }
        }
    }

}