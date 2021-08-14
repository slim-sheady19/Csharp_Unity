using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //tag to execute code outside play mode

[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>(); //Coordinate labeler is script attached to text object which is child of the object where Waypoint script is attached
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
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive(); //set label enabled state to opposite of what it is when C key is hit
        }
    }

    void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
