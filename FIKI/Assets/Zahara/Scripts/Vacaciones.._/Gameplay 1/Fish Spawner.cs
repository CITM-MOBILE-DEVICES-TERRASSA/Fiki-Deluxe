using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    
    public float timeBetweenSpawns;

    private int itemsInSpace = 0;

    void Awake()
    {
    }
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (fishPrefabs.Length == 0) gameObject.SetActive(false);

        while (true)
            yield return StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            while (itemsInSpace > 0)
                yield return null;
            Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cuttable"))
            itemsInSpace++;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Cuttable"))
            itemsInSpace--;
    }
}
