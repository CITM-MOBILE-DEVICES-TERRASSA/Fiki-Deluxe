using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Damage damage;
    [SerializeField] public bool isEnemy = false;
    public ParticleSystem damageParticle;

    private ButtonManager buttonManager;

    private void Start()
    {
        buttonManager = GameObject.FindObjectOfType<ButtonManager>();
    }

    public void Attack()
    {
        if (buttonManager.attack) 
        health.ApplyDamage(damage.GetTotalDamage());
        if(damage.GetTotalDamage() > 50)
        {
            damageParticle.Emit(40);
        }
    }

    public Damage GetDamage() => damage;
}