using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    [SerializeField]
    private float radius = 1f; // Radio del círculo, modificable desde el inspector o el código

    [SerializeField]
    private int segments = 100; // Cantidad de segmentos del círculo, entre más alto, más suave

    private LineRenderer lineRenderer;

    void Awake()
    {
        // Inicializar el LineRenderer solo una vez
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true; // Hace que el círculo se cierre
        UpdateCircle();
    }

    // Método para dibujar el círculo
    void UpdateCircle()
    {
        if (lineRenderer == null)
            return;

        // Configura el número de posiciones en el LineRenderer
        lineRenderer.positionCount = segments + 1;

        // Generar los puntos para el círculo
        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments; // Calcular el ángulo de cada punto
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, transform.position.y, z)); // Colocar el punto en la posición calculada
        }
    }

    // Este método permite actualizar el radio en tiempo real
    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        UpdateCircle();
    }

    // Dibuja el círculo en el editor para vista previa sin ejecutar
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Dibujar los segmentos en el editor
        Vector3 previousPoint = transform.position + new Vector3(Mathf.Sin(0) * radius, 0f, Mathf.Cos(0) * radius);
        for (int i = 1; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            Vector3 currentPoint = transform.position + new Vector3(Mathf.Sin(angle) * radius, 0f, Mathf.Cos(angle) * radius);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }

    // Llama a UpdateCircle() cada vez que se cambian los valores en el inspector
    private void OnValidate()
    {
        UpdateCircle();
    }
}
