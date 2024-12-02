using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceLogic : MonoBehaviour
{
    public AudioClip itemCollected; // Arrastra tu clip de audio aquí en el Inspector
    public GameObject collectParticlesPrefab; // Prefab de partículas para la recogida

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Manager.instance.hasPrice = true;
            Debug.Log("Price has been taken!");

            AudioManager.instance.PlaySFX(itemCollected);

            // Reproducir partículas de recolección
            if (collectParticlesPrefab != null)
            {
                GameObject particles = Instantiate(collectParticlesPrefab, transform.position, Quaternion.identity);

                // Obtener el sistema de partículas y destruirlo después de su duración
                ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    Destroy(particles, particleSystem.main.duration + particleSystem.main.startLifetime.constant);
                }
                else
                {
                    Debug.LogWarning("El prefab de partículas no tiene un sistema de partículas.");
                }
            }
            else
            {
                Debug.LogWarning("Prefab de partículas no asignado en el inspector.");
            }

            // Desactiva la manzana
            gameObject.SetActive(false);
        }
    }
}

