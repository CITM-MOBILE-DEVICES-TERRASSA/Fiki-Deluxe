using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Lifetime Settings")]
    public float lifetime = 5f; // Tiempo de vida de la serpiente en segundos

    [Header("Stretch Animation Settings")]
    public float stretchFrequency = 2f; // Frecuencia de la animación de estiramiento
    public float stretchAmount = 0.1f;  // Intensidad del estiramiento (escala máxima)

    private Vector3 moveDirection;
    private Vector3 originalScale;

    void Start()
    {
        // Guardar la escala original
        originalScale = transform.localScale;

        // Destruir la serpiente automáticamente después de su vida útil
        Destroy(gameObject, lifetime);

        // Rotar la serpiente para apuntar hacia la dirección de movimiento
        UpdateRotation();
    }

    void Update()
    {
        // Mover la serpiente en la dirección asignada
        transform.position += moveDirection * speed * Time.deltaTime;

        // Actualizar la animación de estiramiento
        //UpdateStretchAnimation();
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction.normalized;

        // Actualizar rotación inmediatamente cuando cambie la dirección
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            // Ajustar la escala del sprite en función de la dirección del movimiento
            if (moveDirection.x < 0)
            {
                // Si va hacia la izquierda, invertir el sprite
                transform.localScale = new Vector3(-2, 2, 2);
            }
            else
            {
                // Si va hacia la derecha, mantener la dirección original
                transform.localScale = new Vector3(2, 2, 2);
            }
        }
    }

    private void UpdateStretchAnimation()
    {
        // Usar una onda senoidal para alterar la escala en el eje correspondiente
        float stretch = Mathf.Sin(Time.time * stretchFrequency) * stretchAmount;

        // Actualizar la escala en el eje correspondiente
        transform.localScale = new Vector3(
            originalScale.x + stretch, // Escalar el ancho (X)
            originalScale.y - stretch, // Reducir la altura (Y) para compensar
            originalScale.z // Mantener la profundidad
        );
    }
}
