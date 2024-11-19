using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Manager.instance.hasPrice = true;
            Debug.Log("Price has been taken!");
            this.enabled = false;
        }
    }
}
