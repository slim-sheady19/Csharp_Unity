using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainthrust = 100;
    [SerializeField] float rotation_speed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //cache the rigidbody component in rb on start
        audioSource = GetComponent<AudioSource>(); //cache audiosource
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //if space bar is down
        {
            Debug.Log("space bar");
            rb.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime); //or Vector3.up = 0, 1, 0.  Time.deltaTime to remove frame dependent

            if (!mainBooster.isPlaying)
            {
                mainBooster.Play();
            }

            if (!audioSource.isPlaying) //add sound for thrust
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop(); //stop sound when space bar is released
            mainBooster.Stop();
        }
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.A)) //if A key is down
        {
            apply_rotation(-rotation_speed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            apply_rotation(rotation_speed);
        }
    }

    void apply_rotation(float rotation_this_frame)
    {
        rb.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotation_this_frame * Time.deltaTime); //Vector3.forward = 0, 0, 1
        rb.freezeRotation = false;
    }


}


