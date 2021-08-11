using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } //this getter function is a property of the bool declared one line above.

    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);  //CreateTower method returns bool which we set to is isPlaced
            //Instantiate(towerPrefab, transform.position, Quaternion.identity); //moved this line to tower.cs CreateTower method
            isPlaceable = !isPlaced; //once a tower is placed set the tile to no longer placeable
        }
        
    }
}
