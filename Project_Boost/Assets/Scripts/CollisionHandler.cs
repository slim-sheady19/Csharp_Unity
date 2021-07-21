using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float reload_delay = 2f; //float variable for delay time
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashPFX;
    [SerializeField] ParticleSystem successPFX;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;  // toggle collision
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; } //return stops rest of body from executing
      
            switch (collision.gameObject.tag) //depending on which tag the collided object has, make the switch and add functionality
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    Debug.Log("Congrats, yo, you finished!");
                    levelSuccess();
                    break;
                default: //respawn at start of level
                    Debug.Log("Sorry, you blew up!");
                    start_crash_sequence();
                    break;
            }
        
    }

    void start_crash_sequence()
    {
        GetComponent<Movement>().enabled = false; //disable movement
        audioSource.PlayOneShot(crashSound);
        crashPFX.Play();
        isTransitioning = true;
        Invoke("ReloadLevel", reload_delay); //use invoke to create a delay on the ReloadLevel function }
    }

    void levelSuccess()
    {
        GetComponent<Movement>().enabled = false; //disable movement
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successPFX.Play();
        isTransitioning = true;
        Invoke("LoadNextLevel", reload_delay);
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

