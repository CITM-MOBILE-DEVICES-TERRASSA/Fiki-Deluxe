using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    public GameObject leafPrefab;            // Reference to the leaf prefab
    public int initialLeafCount = 20;        // Initial number of leaves to spawn
    public float spawnInterval = 2f;         // Interval between spawning leaves
    public Vector3 spawnArea = new Vector3(3f, 3f, 3f); // Area range for spawning
    public Transform targetParent;

    public Vector2 randomScaleRange = new Vector2(2.5f, 3.5f);   // Random scale range
    public Vector2 randomRotationRange = new Vector2(-30f, 30f); // Random rotation range

    private float timer;                     // Timer

    void Start()
    {
        // Initial batch spawning of leaves
        for (int i = 0; i < initialLeafCount; i++)
        {
            SpawnLeaf();
        }

        targetParent = GameObject.FindGameObjectWithTag("MainCylindre").transform;
    }

    void Update()
    {
        // Spawn leaves at intervals defined by spawnInterval
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnLeaf();
            timer = 0;
        }
    }

    void SpawnLeaf()
    {
        // Generate random initial position for the leaf
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            transform.position.y,
            //Random.Range(5, spawnArea.y),
            spawnArea.z
        );

        // Instantiate the leaf object
        GameObject leaf = Instantiate(leafPrefab, spawnPosition, Quaternion.identity, targetParent);

        // Set random scale
        float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
        leaf.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // Set random rotation
        float randomRotationZ = Random.Range(randomRotationRange.x, randomRotationRange.y);
        leaf.transform.rotation = Quaternion.Euler(0f, 0f, randomRotationZ);
    }
}

