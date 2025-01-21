using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlataformaController : MonoBehaviour
{
    public static PlataformaController instance;

    [Header("GameObjects")]
    public GameObject metaFinal;
    public GameObject plataforma1Hole;
    public GameObject plataforma2Hole;
    public GameObject ballPrefab; // Referencia a la pelota
    public GameObject plataformaPinchos1Hole;
    public GameObject plataformaPinchos2Hole;
    public GameObject plataformaPinchos1HoleVariant;
    public GameObject plataformaPinchos2HoleVariant;

    private GameObject plataformaToSpawn;

    [Header("Generador")]
    public int numeroPlataforma = 0;
    public float distanciaSpawn = 5f;
    public float startingY = 0;

    private float lastOrientationY = 0;

    [Header("Obstaculos")]
    public GameObject leafSpawner;
    public GameObject netGameObject;
    public GameObject snowBallGameObject;
    public GameObject scytheGameObject;

    public bool aparecerLeafes = false;
    public bool aparecerNets = false;
    public bool aparecerSnowBalls = false;
    public bool aparecerScythes = false;

    private HashSet<int> visitedPlatforms;
    private GameObject ball;

    [Header("Colores de las plataformas")]
    public List<Color> platformColors;

    [Header("Timer")]
    public float countdownTime = 90f;
    public bool timerRunning = true;
    public TextMeshProUGUI countdownText; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        visitedPlatforms = new HashSet<int>();

        //Plataformas
        for (int i = 0; i < numeroPlataforma; i++)
        {
            // Elegir plataforma al azar entre las seis opciones
            int plataformaAleatoria = Random.Range(0, 6);
            if (plataformaAleatoria == 0)
            {
                plataformaToSpawn = plataforma1Hole;
            }
            else if (plataformaAleatoria == 1)
            {
                plataformaToSpawn = plataforma2Hole;
            }
            else if (plataformaAleatoria == 2)
            {
                plataformaToSpawn = plataformaPinchos1Hole;
            }
            else if (plataformaAleatoria == 3)
            {
                plataformaToSpawn = plataformaPinchos2Hole;
            }
            else if (plataformaAleatoria == 4)
            {
                plataformaToSpawn = plataformaPinchos1HoleVariant;
            }
            else
            {
                plataformaToSpawn = plataformaPinchos2HoleVariant;
            }

            // Asignar rotación aleatoria manteniendo una diferencia mínima de 45 grados
            float rotationY = Random.Range(45, 315);
            while (Mathf.Abs(rotationY - lastOrientationY) < 45)
            {
                rotationY = Random.Range(45, 315);
            }
            lastOrientationY = rotationY;

            GameObject newPlatform = Instantiate(plataformaToSpawn,
                new Vector3(0, startingY + (distanciaSpawn * (i + 1)), 0),
                Quaternion.Euler(0, rotationY, 0),
                CylinderController.instance.cylinder.transform);

            AssignRandomColor(newPlatform);

            float proabSpawnObstacle = Random.Range(0, 100);
            if(proabSpawnObstacle < 50)
            {
                int cantidadObstaculos = 0;
                if (aparecerNets) cantidadObstaculos++;
                if (aparecerSnowBalls) cantidadObstaculos++;
                if (aparecerScythes) cantidadObstaculos++;

                if(cantidadObstaculos > 0)
                {
                    proabSpawnObstacle = Random.Range(1, cantidadObstaculos + 1);

                    switch (proabSpawnObstacle)
                    {
                        case 1:
                            if (i != 0 && i != 1)
                            {
                                Instantiate(netGameObject, new Vector3(0, (startingY + (distanciaSpawn * (i + 1)) - (distanciaSpawn / 2)), 0), Quaternion.Euler(0, rotationY, 0), CylinderController.instance.cylinder.transform);
                            }
                            break;
                        case 2:
                            Instantiate(snowBallGameObject, new Vector3(0, (startingY + (distanciaSpawn * (i + 1) - 1) + (1)), 0), Quaternion.Euler(0, rotationY, 0), CylinderController.instance.cylinder.transform);                        //Instantiate(snowBallGameObject, new Vector3(0, (startingY + (distanciaSpawn * (i + 1)) - (distanciaSpawn / 2)), 0), Quaternion.Euler(0, rotationY, 0), CylinderController.instance.cylinder.transform);
                            break;
                        case 3:
                            Instantiate(scytheGameObject, new Vector3(0, (startingY + (distanciaSpawn * (i + 1) - 0.4f)), 0), Quaternion.Euler(0, rotationY, 0), CylinderController.instance.cylinder.transform);
                            Debug.Log("Spawn scythe");
                            break;
                    }
                }
                
            }
        }

        if (aparecerLeafes)
        {
            Instantiate(leafSpawner, new Vector3(0, startingY + (distanciaSpawn * numeroPlataforma), 0), Quaternion.identity, CylinderController.instance.cylinder.transform);
        }

        // Instanciar la meta final
        Instantiate(metaFinal, new Vector3(0, startingY + (distanciaSpawn * (numeroPlataforma + 1)), 0), Quaternion.identity, CylinderController.instance.cylinder.transform);

        // Instanciar pelota
        ball = Instantiate(ballPrefab, new Vector3(0, 0.4f, -0.7f), Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            countdownTime -= Time.deltaTime;
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                timerRunning = false;
            }
        }

        // Update the countdown text
        countdownText.text = Mathf.Ceil(countdownTime).ToString();

        CheckBallPosition();
        //CheckBallReachedGoal();
    }

    private void AssignRandomColor(GameObject platform)
    {
        if (platformColors != null && platformColors.Count > 0)
        {
            Color randomColor = platformColors[Random.Range(0, platformColors.Count)];
            Renderer[] renderers = platform.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.CompareTag("Plataforma"))
                {
                    Material newMaterial = new Material(renderer.material);
                    newMaterial.SetColor("_BaseColor", randomColor);
                    renderer.material = newMaterial;

                    Debug.Log($"Color asignado a {renderer.gameObject.name} con etiqueta Plataforma: {randomColor}");
                }
            }
        }
    }

    void CheckBallPosition()
    {
        if (ball != null)
        {
            float ballY = ball.transform.position.y;
            float currentPlatform = Mathf.Floor(ballY / distanciaSpawn);
            float platformBaseY = currentPlatform * distanciaSpawn;
            float newTargetY = startingY + platformBaseY;

            if (currentPlatform > 0 && ballY >= platformBaseY + 0.2f && ballY <= platformBaseY + 0.6f && !visitedPlatforms.Contains((int)currentPlatform))
            {
                visitedPlatforms.Add((int)currentPlatform);
                Score.Instance.AddScore(20);
                Debug.Log("Score: + 20");
            }
        }
    }

    // void CheckBallReachedGoal()
    // {
    //     if (ball != null && metaFinal != null)
    //     {
    //         float distanceToGoal = Vector3.Distance(ball.transform.position, metaFinal.transform.position);
    //         if (distanceToGoal < 1.0f)
    //         {
    //             timerRunning = false;
    //             int bonusScore = Mathf.CeilToInt(countdownTime) * 10;
    //             Score.Instance.AddScore(bonusScore);
    //             Debug.Log("Bonus Score: " + bonusScore);
    //         }
    //     }
    // }
}