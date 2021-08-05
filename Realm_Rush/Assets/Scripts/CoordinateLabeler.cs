using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //tag to execute code outside play mode

public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!Application.isPlaying) //ensure we are not in play mode
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); //Mathf function to convert to int since variable coordinates is int
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); //using Z as Y because 2-D plane in game is using X,Z coords in editor
        //the division is for coords to be 1, 2 instead of 10, 20 etc

        label.text = coordinates.x + ", " + coordinates.y;
    }

    void UpdateObjectName() //update the name in hierarchy while still in edit mode
    {
        transform.parent.name = coordinates.ToString();
    }
}
