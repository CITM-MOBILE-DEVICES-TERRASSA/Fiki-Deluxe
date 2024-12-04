using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player; // El objeto del personaje a seguir
    public float minX; // Límite izquierdo
    public float maxX; // Límite derecho
    public float minY; // Límite inferior
    public float maxY; // Límite superior

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private void Update()
    {

        // Calcula la mitad del tamaño de la cámara en X e Y
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        // Calcula la posición deseada de la cámara en base a la posición del jugador
        float targetX = Mathf.Clamp(player.position.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);
        float targetY = Mathf.Clamp(player.position.y, minY + cameraHalfHeight, maxY - cameraHalfHeight);

        // Aplica la nueva posición de la cámara
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
