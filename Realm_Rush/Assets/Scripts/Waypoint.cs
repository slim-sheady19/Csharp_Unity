using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } //this getter function is a property of the bool declared one line above.

    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null) //don't do an isvalid check in start
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);  //CreateTower method returns bool which we set to is isPlaced
            //Instantiate(towerPrefab, transform.position, Quaternion.identity); //moved this line to tower.cs CreateTower method
            isPlaceable = !isPlaced; //once a tower is placed set the tile to no longer placeable
            gridManager.BlockNode(coordinates);
        }
        
    }
}
