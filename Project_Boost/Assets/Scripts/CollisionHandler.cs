using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) //depending on which tag the collided object has, make the switch and add functionality
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                Debug.Log("Congrats, yo, you finished!");
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("You picked up fuel");
                break;
            default: //respawn at start of level
                Debug.Log("Sorry, you blew up!");
                ReloadLevel();
                break;
        }
    }
    void ReloadLevel()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex; //store in variable to make code more readable
        SceneManager.LoadScene(current_scene_index);
    }

    void LoadNextLevel()
    {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        int next_scene_index = current_scene_index + 1;
        
        if (next_scene_index == SceneManager.sceneCountInBuildSettings)
        {
            next_scene_index = 0;
        }

        SceneManager.LoadScene(next_scene_index);

    }
}

