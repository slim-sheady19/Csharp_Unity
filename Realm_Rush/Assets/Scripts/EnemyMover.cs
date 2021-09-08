using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMover : MonoBehaviour
{
    //[SerializeField] List<Waypoint> path = new List<Waypoint>(); //list is a vector?.  create list of Waypoint objects called path. (this line being replaced with Nodes below)
    List<Node> path = new List<Node>();

    [SerializeField] [Range(0f, 5f)] float speed = 1f; //range to prevent negative numbers

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathfinder;

    void OnEnable()
    {
        //Debug.Log("Start Here");
        ReturnToStart();
        RecalculatePath(true);
        
        //Debug.Log("finishing start");
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath) 
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines(); //stop coroutine before getting new path, reenable after
        path.Clear(); //clear the list before adding the new path
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath()); //calling coroutine is somewhat different

        /*  OLD CODE BEFORE ADDING BREADTHFIRSTSEARCH PATHFINDING AND NODES
         *  GameObject parent = GameObject.FindGameObjectWithTag("Path");

          foreach(Transform child in parent.transform) //add path to list in order.  for this to work they must be selected in the right order in the hierarchy
          {
              Waypoint waypoint = child.GetComponent<Waypoint>();

              if(waypoint != null)
              {
                  path.Add(waypoint);
              }

          }*/
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)//OLD: foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            //rotate enemy to be facing direction of path
            transform.LookAt(endPos);

            //have enemy move smoothly from tile to tile using lerp using while loop instead of update
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame(); //coroutine, see below.  WaitForEndOfFrame ensures it does not move too quickly
            }
        }
        FinishPath();
    }
}

/*this line tells Unity to go complete other code and return again after the frame to execute on the next element in the list
            in this case the coroutine executes after the first log statement in Start(), acts on the first element in the path list, goes back to Start and finishes the code
            "finishing start" and then executes on all the rest of the elements in the list*/