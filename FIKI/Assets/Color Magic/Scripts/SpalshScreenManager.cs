using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpalshScreenManager : MonoBehaviour
{    
    private void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {       
        yield return new WaitForSeconds(6f);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
