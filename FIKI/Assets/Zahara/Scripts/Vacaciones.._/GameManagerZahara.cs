using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerZahara : MonoBehaviour
{
    #region Singleton
    private static GameManagerZahara _instance;
    public static GameManagerZahara Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    #endregion
}
