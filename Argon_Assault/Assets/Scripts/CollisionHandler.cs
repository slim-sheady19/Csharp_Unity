using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
    //Note: Both GameObjects must contain a Collider component. One must have Collider.isTrigger enabled, and contain a Rigidbody.
    //If both GameObjects have Collider.isTrigger enabled, no collision happens. The same applies when both GameObjects do not have a Rigidbody component.

    [SerializeField] ParticleSystem explosionPS;

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log(this.name + "Collided with" + other.gameObject.name);

        GetComponent<PlayerControls>().enabled = false; //disable controls


        //trigger explosion and make mesh disappear, disable box collider
        explosionPS.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        //reload scene
        Invoke("ReloadLevel", 1);
    }

    void ReloadLevel()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex; //store in variable to make code more readable
        SceneManager.LoadScene(current_scene_index);
    }
}
