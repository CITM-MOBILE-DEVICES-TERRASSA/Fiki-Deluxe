using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceLogic : MonoBehaviour
{
    public AudioClip itemCollected;
    public GameObject collectParticlesPrefab;
    [SerializeField] private TimerScript timer;

    private void Start()
    {
        timer = FindObjectOfType<TimerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Manager.instance.hasPrice = true;
            Debug.Log("Price has been taken!");

            AudioManager.instance.PlaySFX(itemCollected);
            Manager.instance.score += 100 + 2 * (int)timer.timeRemaining;

            if (collectParticlesPrefab != null)
            {
                GameObject particles = Instantiate(collectParticlesPrefab, transform.position, Quaternion.identity);

                ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    Destroy(particles, particleSystem.main.duration + particleSystem.main.startLifetime.constant);
                }
                else
                {
                    Debug.LogWarning("El prefab de particulas no tiene un sistema de particulas.");
                }
            }
            else
            {
                Debug.LogWarning("Prefab de particulas no asignado en el inspector.");
            }

            gameObject.SetActive(false);
        }
    }
}

