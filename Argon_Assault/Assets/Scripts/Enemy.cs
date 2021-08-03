using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject damageFX;
    

    [SerializeField] int enemyHealth = 10;

    [SerializeField] int hitPoints = 1;
    [SerializeField] int killPoints = 10;

    //before this lesson (98), parent variable was declared here as an object of Type transform which immediately gave us access to transform properties
    //now that we change declaration to type GameObject we use the dot(.) operator below (where we are dynamically spawning the pfx) to access transform properties
    GameObject parentGameObject;
    ScoreBoard score; //do not serialize field otherwise we would have to manually point to score on every enemy

    void Start()
    {
        score = FindObjectOfType<ScoreBoard>(); //FindObjectOfType to find the first object in the assets of this type, store it in score
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        
    }

    void OnParticleCollision(GameObject other)
    {
        
        EnemyTakeDamage();
        
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }       
    }

    void EnemyTakeDamage()
    {
        //spawn and destroy damage effect
        GameObject dfx = Instantiate(damageFX, transform.position, Quaternion.identity);
        dfx.transform.parent = parentGameObject.transform;

        enemyHealth--;
        score.IncreaseScore(hitPoints);
    }

    void EnemyDeath()
    {
        //dynamically spawn explosion at enemy location
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //this will play the child component audio source explosion sound on awake as well
        fx.transform.parent = parentGameObject.transform; //parent it to variable parent as to not clog up hierarchy

        Destroy(gameObject); //gameObject just used as placeholder for argument, it is not referring to input paramaters for function above.  "this" does not work either
        score.IncreaseScore(killPoints); //call increase score function from score object
    }
}
