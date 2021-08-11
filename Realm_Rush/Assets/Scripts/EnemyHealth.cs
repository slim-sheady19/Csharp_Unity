using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints = 0;

    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();    
    }

    void OnParticleCollision()
    {
        TakeDamage(1);
    }

    void TakeDamage(int damage)
    {
        currentHitPoints -= damage;
        Debug.Log("TakeDamage called, currentHitPoints = " + currentHitPoints);
        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}