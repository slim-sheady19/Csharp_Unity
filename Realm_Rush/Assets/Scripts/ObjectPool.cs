using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] [Range(0, 50)] int poolSize = 5; //range to prevent negative numbers
    [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

    GameObject[] pool;

    void Awake() //populate the enemy pool in Awake in case any other code needs these objects before Start
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyToSpawn, transform); //transform argument sets ObjectPool empty GO to be the spawned enemy's parent
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        foreach(GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while(Application.isPlaying)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
