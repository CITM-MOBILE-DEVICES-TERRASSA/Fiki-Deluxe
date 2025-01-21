using UnityEngine;

public class ScytheRotator : MonoBehaviour
{
    // Velocidad de rotación en grados por segundo
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rota el objeto continuamente en el eje Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

