using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>(); //list is a vector?.  create list of Waypoint objects called path
    [SerializeField] [Range(0f, 5f)] float speed = 1f; //range to prevent negative numbers

    Enemy enemy;

    void OnEnable()
    {
        Debug.Log("Start Here");
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath()); //calling coroutine is somewhat different
        Debug.Log("finishing start");
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath() 
    {
        path.Clear(); //clear the list before adding the new path

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
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
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}

/*this line tells Unity to go complete other code and return again after the frame to execute on the next element in the list
            in this case the coroutine executes after the first log statement in Start(), acts on the first element in the path list, goes back to Start and finishes the code
            "finishing start" and then executes on all the rest of the elements in the list*/