using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic: MonoBehaviour
{
    public AudioClip itemCollected;
    public float powerUpDuration = 5f;
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Manager.instance.hasPrice = true;
            Debug.Log("Price has been taken!");

            AudioManager.instance.PlaySFX(itemCollected);
            player.ActivateDoubleJump(powerUpDuration);

            gameObject.SetActive(false);
        }
    }
}

