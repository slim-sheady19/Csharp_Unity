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
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f); //orange isn't built in

   TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
  

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>(); //set gridManager variable to the one GridManager in the scene
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
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
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; } //is valid check before proceeding

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath) //path must come before explored since status of isPath is set once explored
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        /*OLD VERSION
         * coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); //Mathf function to convert to int since variable coordinates is int
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); //using Z as Y because 2-D plane in game is using X,Z coords in editor
        //the division is for coords to be 1, 2 instead of 10, 20 etc*/

        label.text = coordinates.x + ", " + coordinates.y;
    }

    void UpdateObjectName() //update the name in hierarchy while still in edit mode
    {
        transform.parent.name = coordinates.ToString();
    }
}
