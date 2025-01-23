using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    public GameObject elefantePrefab;
    public GameObject monarcaPrefab;
    public GameObject pedroSanchezPrefab;
    public GameObject reinaSofiaPrefab;
        
    public Canvas spawnCanvas;

    // Duracion total del spawn en segundos (2 minutos y medio = 150 segundos)
    [SerializeField] private float totalSpawnDuration = 150f;


    // Intervalo de spawn (medio segundo para elefantes, 1 segundo para monarcas)
    private float elefanteSpawnInterval = 2.0f;
    private float monarcaSpawnInterval = 4.0f;
        
    private int pedroSanchezSpawnCount = 0;
    private int maxPedroSanchezCount = 3;
    private bool hasSpawnedReinaSofia = false;

    void Start()
    {        
        StartCoroutine(SpawnElefantes());
        StartCoroutine(SpawnMonarcas());
        StartCoroutine(SpawnSpecialCharacters());
    }

    IEnumerator SpawnElefantes()
    {
        float elapsedTime = 0f;
        while (elapsedTime < totalSpawnDuration)
        {
            Spawn(elefantePrefab);
            yield return new WaitForSeconds(elefanteSpawnInterval);
            elapsedTime += elefanteSpawnInterval;
        }
    }

    IEnumerator SpawnMonarcas()
    {
        float elapsedTime = 0f;
        while (elapsedTime < totalSpawnDuration)
        {
            Spawn(monarcaPrefab);
            yield return new WaitForSeconds(monarcaSpawnInterval);
        }
    }

    IEnumerator SpawnSpecialCharacters()
    {
        float elapsedTime = 0f;
        float specialCharacterSpawnInterval = 15.0f;

        while (elapsedTime < totalSpawnDuration)
        {
            //Pedro Sanchez (max 3)
            if (pedroSanchezSpawnCount < maxPedroSanchezCount)
            {
                Spawn(pedroSanchezPrefab);
                pedroSanchezSpawnCount++;
            }

            //Reina Sofia (solo 1 vez)
            if (!hasSpawnedReinaSofia)
            {
                Spawn(reinaSofiaPrefab);
                hasSpawnedReinaSofia = true;
            }

            yield return new WaitForSeconds(specialCharacterSpawnInterval);
            elapsedTime += specialCharacterSpawnInterval;
        }
    }

    void Spawn(GameObject prefab)
    {
        //generar pos aleatoria dentro del Canvas
        RectTransform canvasRect = spawnCanvas.GetComponent<RectTransform>();
                
        float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float y = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2 - 375);
                
        GameObject spawnedObject = Instantiate(prefab, spawnCanvas.transform);

        // Ajusta pos del objeto en el espacio del Canvas
        RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
                
        rectTransform.SetAsLastSibling();  // coloca objeto en el top layer del Canvas
                
        Destroy(spawnedObject, 5f);
    }
}
