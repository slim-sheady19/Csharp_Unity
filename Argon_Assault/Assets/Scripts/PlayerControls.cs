using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float YRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() //OnEnable is a system function like start and update called between the two. https://docs.unity3d.com/Manual/ExecutionOrder.html
    {
        
        {
            movement.Enable(); //new input system needs to be explicitly enabled in OnEnable
        }
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;

        // for new position we take local position, add movement input xThrow, control for time and multiply by control speed so it's not too slow.  do the same for Y
        float newX = transform.localPosition.x + xThrow * Time.deltaTime * controlSpeed; 
        float newY = transform.localPosition.y + yThrow * Time.deltaTime * controlSpeed;

        //clamp x and Y values
        float newXClamp = Mathf.Clamp(newX, -xRange, xRange);
        float newYClamp = Mathf.Clamp(newY, 0, YRange);

        //use new position values in transform on every frame
        transform.localPosition = new Vector3(newXClamp, newYClamp, transform.localPosition.z);

    }
}
