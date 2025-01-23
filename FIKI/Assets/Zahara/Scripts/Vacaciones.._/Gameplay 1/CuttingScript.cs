using System;
using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    public GameObject fish;
    private Fish cutable;
    public GameObject sangre;
    private ScoreManagerLevel scoremanagerlevel;
    [Min(1)]public int maxScorePerCut = 200;
    // Relacion inversamente proporcional de la distancia
    [Range(0.01f,10.0f)]public float distanceFactor = 2.0f;

    //animacion cuchillo
    public float cutAnimDuration = 0.3f;
    public float cutAngle = 45f;
    private Quaternion initialRotation;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        cutable = FindObjectOfType<Fish>(); 
        scoremanagerlevel = FindObjectOfType<ScoreManagerLevel>();
        initialRotation = transform.GetChild(0).rotation;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(0)) //left button
        {
            PerformCut();
        }
    }

    private IEnumerator CutKnifeAnim()
    {
        float elapsedTime = 0f;

        Transform pivot = transform.GetChild(0);
        
        float initAngle = initialRotation.eulerAngles.x;
        //rotar cuchillo
        while (elapsedTime < cutAnimDuration)
        {
            float t = elapsedTime / cutAnimDuration;
            float angle = Mathf.Lerp(0f, cutAngle, t);
            pivot.rotation = Quaternion.Euler(initAngle-angle, 0f, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //devolver cuchillo a pos inicial
        elapsedTime = 0f;
        while (elapsedTime < cutAnimDuration)
        {
            float t = elapsedTime/cutAnimDuration;
            float angle = Mathf.Lerp(cutAngle, 0f, t);
            pivot.rotation = Quaternion.Euler(initAngle-angle, 0f, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pivot.rotation = initialRotation;
    }

    public void PerformCut()
    {
        // Convertir a corutina y añadir un delay para sincronizar con la animacion?

        if (fish == null) return;

        //corutina para anim de corte
        StartCoroutine(CutKnifeAnim());

        // Realizamos el sonido del corte
        if (audioSource != null)
        {
            audioSource.Play();
        }
        // Realizamos la operacion de corte
        var m = CSG.Subtract(fish, gameObject);

        fish.GetComponent<MeshFilter>().sharedMesh = m.mesh;
        fish.GetComponent<MeshRenderer>().sharedMaterials = m.materials.ToArray();

        // Recalcula la colision del objeto cortado
        Destroy(fish.GetComponent<Collider>());
        fish.AddComponent<MeshCollider>().convex = true;
        fish.transform.position = Vector3.zero;

        var fishScript = fish.GetComponent<Fish>();
        var marker = fishScript.instance.transform;
        if (fishScript != null)
        {
            fishScript.PlayCutSound();
        }


        if (sangre != null)
        {
            var particleSystem = sangre.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                // Reproducir el sistema de partículas
                particleSystem.Play();
            }
        }

        if (marker != null)
        {
            // Calculamos la distancia en el eje X
            var distanceX = Mathf.Abs(transform.position.x - marker.position.x);
        
            // Calculamos la puntuación proporcional a la distancia absoluta
            var cutScore = Mathf.RoundToInt(maxScorePerCut/(1+(distanceX*distanceFactor)));
            scoremanagerlevel.UpdateScoreLevel1(cutScore);
        }
        
        fishScript.effect.ApplyEffect();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cuttable"))
        {
            fish = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == fish)
        {
            fish = null;
        }
    }
}
