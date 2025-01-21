using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager Instance;

    public bool[] availableLevels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
            InitializeLevels();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLevels()
    {
        int totalLevels = 5; // Cambia esto al número total de niveles
        availableLevels = new bool[totalLevels];
        availableLevels[0] = true; // Primer nivel desbloqueado por defecto

        // Opcional: Cargar progreso guardado (si quieres persistencia)
        for (int i = 0; i < totalLevels; i++)
        {
            string key = $"Level_{i}_Unlocked";
            availableLevels[i] = PlayerPrefs.GetInt(key, i == 0 ? 1 : 0) == 1;
        }
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < availableLevels.Length)
        {
            availableLevels[levelIndex] = true;

            // Guardar progreso (opcional)
            PlayerPrefs.SetInt($"Level_{levelIndex}_Unlocked", 1);
            PlayerPrefs.Save();
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        return levelIndex >= 0 && levelIndex < availableLevels.Length && availableLevels[levelIndex];
    }
}
