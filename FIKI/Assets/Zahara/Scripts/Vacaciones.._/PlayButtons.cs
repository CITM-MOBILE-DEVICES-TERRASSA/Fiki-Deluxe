using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtons : MonoBehaviour
{
    public GameObject playButton;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMinigameOne()
    {
        audioSource.Play();
        SceneLoader.Instance.LoadMeta();
    }

    public void PlayMinigameTwo()
    {
        audioSource.Play();
        SceneLoader.Instance.LoadMeta1();
    }
}
