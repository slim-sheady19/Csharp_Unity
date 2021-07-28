using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")] //Header attritbute that lasts until the next header
    [Tooltip("How fast ship moves up and down")][SerializeField] InputAction movement; //Tooltip is a mouse over text to explain what we've coded in the editor
    [SerializeField] InputAction fire;
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("How far player moves horizontally")] [SerializeField] float xRange = 10f;
    [Tooltip("How far player moves vertically")] [SerializeField] float YRange = 10f;

    [Header("Laser gun array")]
    [SerializeField] GameObject[] lasers; //declare array of type Gameobject that gameobjects can be added to in editor

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;


    float yThrow;
    float xThrow;
    float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() //OnEnable is a system function like start and update called between the two. https://docs.unity3d.com/Manual/ExecutionOrder.html
    {
        fire.Enable();
        movement.Enable(); //new input system needs to be explicitly enabled in OnEnable
        
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        Attack();

    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x; //from movement input system
        yThrow = movement.ReadValue<Vector2>().y;

        // for new position we take local position, add movement input xThrow, control for time and multiply by control speed so it's not too slow.  do the same for Y
        float newX = transform.localPosition.x + xThrow * Time.deltaTime * controlSpeed;
        float newY = transform.localPosition.y + yThrow * Time.deltaTime * controlSpeed;

        //clamp x and Y values
        float newXClamp = Mathf.Clamp(newX, -xRange, xRange);
        float newYClamp = Mathf.Clamp(newY, 0, YRange);

        //use new position values in transform on every frame
        transform.localPosition = new Vector3(newXClamp, newYClamp, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //https://en.wikipedia.org/wiki/Quaternion
    }

    void Attack()
    {
       
       if (fire.ReadValue<float>() > 0.5) //read value from input system
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
        
    }


    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
