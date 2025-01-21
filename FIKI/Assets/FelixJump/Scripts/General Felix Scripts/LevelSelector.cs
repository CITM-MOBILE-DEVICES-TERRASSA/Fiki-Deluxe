using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private static LevelSelector _instance;
    public static LevelSelector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelSelector>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("LevelSelector");
                    _instance = singleton.AddComponent<LevelSelector>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] public Button[] levelButtons = new Button[5];
    [SerializeField] public bool[] availableLevels = new bool[5];
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject levelsPanel;

    private void Awake()
    {
        // Evita la creación de múltiples instancias
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        // No destruir este objeto entre escenas
        DontDestroyOnLoad(gameObject);

        // Volver a asignar los GameObjects si es necesario
        
            // Esto se ejecuta solo cuando estás en la escena de LevelSelector.
            
        

        // Si no estamos en la escena de nivel, configurar los botones
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            for (int i = 0; i < availableLevels.Length; i++)
            {
                if (availableLevels[i] == availableLevels[0])
                {
                    availableLevels[i] = true;
                }
            }

            
        }
    }

    public void SelectLevel(string levelName)
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.ResumeGame();
        ScenesManager.Instance.LoadScene(levelName);
    }

    public void ChangeToLevelSelector()
    {
        
        {
            levelButtons[0] = GameObject.Find("Spring").GetComponent<Button>();
            levelButtons[1] = GameObject.Find("Summer").GetComponent<Button>();
            levelButtons[2] = GameObject.Find("Autumn").GetComponent<Button>();
            levelButtons[3] = GameObject.Find("Winter").GetComponent<Button>();
            levelButtons[4] = GameObject.Find("Halloween").GetComponent<Button>();
        }
        if (gamePanel == null || levelsPanel == null)
        {
            gamePanel = GameObject.Find("PlayGame");
            levelsPanel = GameObject.Find("Canvas");
            levelButtons[0].onClick.AddListener(() => SelectLevel("SpringLevel"));
            levelButtons[1].onClick.AddListener(() => SelectLevel("SummerLevel"));
            levelButtons[2].onClick.AddListener(() => SelectLevel("AutumnLevel"));
            levelButtons[3].onClick.AddListener(() => SelectLevel("WinterLevel"));
            levelButtons[4].onClick.AddListener(() => SelectLevel("HalloweenLevel"));

        }
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = availableLevels[i];
        }
        gamePanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
}
