using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    // using Sin wave for oscillation - see lecture 57.
    void Update()
    {
        //so we dont execute when period = 0 so we dont divide by 0 and create error.  Mathf.Epsilon so the floats are not compared too precisely, see lecture 58.
        if (period <= Mathf.Epsilon) 
        {
            float cycles = Time.time / period; //continually growing over time

            const float tau = Mathf.PI * 2; //constant value of 6.283
            float rawSinWave = Mathf.Sin(cycles * tau); //range -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f; //recalculated to go from 0-1 so it's cleaner

            Vector3 offset = movementVector * movementFactor;
            transform.position = (startingPosition + offset);
        }
    }
}
