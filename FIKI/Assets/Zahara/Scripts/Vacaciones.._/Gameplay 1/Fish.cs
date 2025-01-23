using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;



public class Fish : MonoBehaviour
{
    [FormerlySerializedAs("prefab")] public GameObject markPrefab;
    public GameObject instance;

    public FishEffectBase effect;

    private AudioSource audioSource;

   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
        CreatePoint();
    }
    public void PlayCutSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
       
    }



    private void CreatePoint()
        {
            if (markPrefab == null)
            {
                Debug.LogError("No se ha asignado un prefab en el inspector.");
                return;
            }

            Renderer renderer = GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogError("El objeto no tiene un componente Renderer para calcular los l�mites.");
                return;
            }

            float minX = renderer.bounds.min.x;
            float maxX = renderer.bounds.max.x;

            float randomX = Random.Range(minX, maxX);
            float randomy = 0.5f;

            Vector3 spawnPosition = new Vector3(randomX, transform.position.y + randomy, transform.position.z);

            instance = Instantiate(markPrefab, spawnPosition, Quaternion.identity);
            instance.transform.parent = gameObject.transform;
        }

    

}
