using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int killPoints = 10;

    ScoreBoard score; //do not serialize field otherwise we would have to manually point to score on every enemy

    void Start()
    {
        score = FindObjectOfType<ScoreBoard>(); //FindObjectOfType to find the first object in the assets of this type, store it in score
    }

    void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent; //parent it to variable parent
        Destroy(gameObject); //gameObject just used as placeholder for argument, it is not referring to input paramaters for function above.  "this" does not work either

        score.IncreaseScore(killPoints); //call increase score function from score object
    }
}
