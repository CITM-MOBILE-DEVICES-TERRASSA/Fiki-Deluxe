using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public enum Game
{
    Fiki,
    JumpingJack,
    GAME_COUNT
}

// Ayudante para poder inicializar correctamente las listas
[Serializable]
public class ScoreDataList
{
    [FormerlySerializedAs("scoreList")] public List<int> l = new List<int>();
}

// Almacenamiento de puntuación para múltiples juegos. Añade el juego al enum "Game" y llama a la función con el identificador de juego y el nivel (empezando por 0)
// No está terminado porque el código existente para puntuación en los dos juegos que había al momento de implementar esto va más allá de código espagueti
[Serializable]
public class ScoreData
{
    private static ScoreData _instance;

    public static ScoreData Instance => _instance ??= new ScoreData();

    public ScoreDataList[] scores = new ScoreDataList[(int)Game.GAME_COUNT];


    ScoreData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        try
        {
            var json = File.ReadAllText(filePath);
            if (json != "")
                JsonUtility.FromJsonOverwrite(json, this);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log($"Archivo de guardado no encontrado en {filePath}");
        }
    }

    ~ScoreData()
    {
        var json = JsonUtility.ToJson(this, true);
        string filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        File.WriteAllText(filePath, json);
        Debug.Log($"Puntuaciones guardadas en {filePath}");
    }
    
    // Intenta guardar la puntuación máxima. Supone que los niveles nunca serán jugados fuera de orden o ya hay high score para todos los niveles
    public void SetHighScore(Game game, int level, int score)
    {
        var id = (int)game;
        if (scores[id].l.Count < level)
            scores[id].l.Add(score);
            
        if (scores[id].l[level] < score)    
            scores[id].l[level] = score;
    }

    public int GetHighScore(Game game, int level)
    {
        return scores[(int)game].l[level];
    }

    // Devuelve la suma de todas las puntuaciones del juego
    public int GetTotalScore(Game id)
    {
        return scores[(int)id].l.Sum();
    }
}
