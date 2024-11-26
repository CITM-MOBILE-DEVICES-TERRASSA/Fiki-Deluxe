using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceLogic : MonoBehaviour
{

    public AudioClip itemCollected; // Arrastra tu clip de audio aquí en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manager.instance.hasPrice = true;
            Debug.Log("Price has been taken!");
            gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(itemCollected);
        }
    }
}
