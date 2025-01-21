using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;  // La barra de salud o cualquier slider.
    public float animationSpeed = 2f; // Velocidad de la animación.

    private Coroutine currentAnimation; // Para detener animaciones previas.

    // Método para cambiar el valor con animación.
    public void SetBarValue(float newValue)
    {
        // Si ya hay una animación corriendo, la detenemos.
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }

        // Iniciamos una nueva animación.
        currentAnimation = StartCoroutine(AnimateBar(newValue));
    }

    private IEnumerator AnimateBar(float targetValue)
    {
        // Obtenemos el valor actual de la barra.
        float startValue = healthBar.value;

        // Tiempo acumulado para interpolar.
        float elapsedTime = 0f;

        // Animación suave.
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * animationSpeed;

            // Interpolamos entre el valor actual y el objetivo.
            healthBar.value = Mathf.Lerp(startValue, targetValue, elapsedTime);

            yield return null; // Esperamos un frame.
        }

        // Aseguramos que la barra llegue exactamente al valor objetivo.
        healthBar.value = targetValue;
    }
}
