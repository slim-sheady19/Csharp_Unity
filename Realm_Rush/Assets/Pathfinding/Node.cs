using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //serialize the class since it's a pure class

public class Node //removed monobehaviour inheritance
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable) //constructor
    {
        this.coordinates = coordinates; //this keyword referring to the Vector2Int coordinates being passed in to the constructor (and assigning to public coordinates)
        this.isWalkable = isWalkable; //same as above
    }
}
