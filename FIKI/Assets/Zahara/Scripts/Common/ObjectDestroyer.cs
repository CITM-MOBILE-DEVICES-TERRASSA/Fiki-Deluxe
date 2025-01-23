using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField,Min(0.0f)] private float timeToDestroy = 5f;

    private void OnCollisionExit(Collision other)
    {
        Destroy(other.gameObject, timeToDestroy);
    }
}
